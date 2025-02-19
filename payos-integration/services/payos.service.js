const axios = require('axios');
const crypto = require('crypto');
const payosConfig = require('../config/payos.config');

class PayOSService {
    constructor() {
        this.config = payosConfig;
    }

    createSignature(data) {
        const signData = `${data.orderCode}|${data.amount}|${data.description}|${data.cancelUrl}|${data.returnUrl}`;
        return crypto
            .createHmac('sha256', this.config.checksumKey)
            .update(signData)
            .digest('hex');
    }

    async createPaymentRequest(orderData) {
        try {
            const paymentData = {
                orderCode: orderData.orderCode,
                amount: orderData.amount,
                description: orderData.description,
                cancelUrl: this.config.cancelUrl,
                returnUrl: this.config.returnUrl,
                signature: this.createSignature({
                    orderCode: orderData.orderCode,
                    amount: orderData.amount,
                    description: orderData.description,
                    cancelUrl: this.config.cancelUrl,
                    returnUrl: this.config.returnUrl,
                })
            };

            const response = await axios.post(this.config.apiUrl, paymentData, {
                headers: {
                    'x-client-id': this.config.clientId,
                    'x-api-key': this.config.apiKey,
                }
            });

            return response.data;
        } catch (error) {
            throw new Error(`Payment request failed: ${error.message}`);
        }
    }

    verifyWebhookSignature(payload, signature) {
        const calculatedSignature = crypto
            .createHmac('sha256', this.config.checksumKey)
            .update(JSON.stringify(payload))
            .digest('hex');
        
        return calculatedSignature === signature;
    }
}

module.exports = new PayOSService(); 