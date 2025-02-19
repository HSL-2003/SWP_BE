const payosService = require('../services/payos.service');
const sql = require('mssql');

class PaymentController {
    async createPayment(req, res) {
        try {
            const { orderId, amount, description } = req.body;
            const orderCode = `ORDER_${orderId}_${Date.now()}`;

            const paymentResponse = await payosService.createPaymentRequest({
                orderCode,
                amount,
                description
            });

            await sql.query`
                INSERT INTO Payment (OrderId, Amount, PaymentStatus, PaymentDate)
                VALUES (${orderId}, ${amount}, 'PENDING', GETDATE())
            `;

            res.json(paymentResponse);
        } catch (error) {
            res.status(500).json({ error: error.message });
        }
    }

    async handleWebhook(req, res) {
        try {
            const signature = req.headers['x-payos-signature'];
            const payload = req.body;

            if (!payosService.verifyWebhookSignature(payload, signature)) {
                return res.status(400).json({ error: 'Invalid signature' });
            }

            const orderIdMatch = payload.orderCode.match(/ORDER_(\d+)_/);
            const orderId = orderIdMatch ? orderIdMatch[1] : null;

            if (!orderId) {
                throw new Error('Invalid order code format');
            }

            switch (payload.status) {
                case 'PAID':
                    await sql.query`
                        UPDATE Payment 
                        SET PaymentStatus = 'PAID'
                        WHERE OrderId = ${orderId}
                    `;

                    await sql.query`
                        INSERT INTO PaymentHistory (PaymentId, Status, CreatedDate)
                        SELECT PaymentId, 'PAID', GETDATE()
                        FROM Payment
                        WHERE OrderId = ${orderId}
                    `;
                    break;

                case 'CANCELLED':
                    await sql.query`
                        UPDATE Payment 
                        SET PaymentStatus = 'CANCELLED'
                        WHERE OrderId = ${orderId}
                    `;

                    await sql.query`
                        INSERT INTO PaymentHistory (PaymentId, Status, CreatedDate)
                        SELECT PaymentId, 'CANCELLED', GETDATE()
                        FROM Payment
                        WHERE OrderId = ${orderId}
                    `;
                    break;

                default:
                    await sql.query`
                        UPDATE Payment 
                        SET PaymentStatus = ${payload.status}
                        WHERE OrderId = ${orderId}
                    `;
            }

            res.json({ message: 'Webhook processed successfully' });
        } catch (error) {
            console.error('Webhook error:', error);
            res.status(500).json({ error: error.message });
        }
    }
}

module.exports = new PaymentController(); 