const express = require('express');
const bodyParser = require('body-parser');
const paymentRoutes = require('./routes/payment.routes');

const app = express();
const PORT = process.env.PORT || 3000;

app.use(bodyParser.json());

// Routes
app.use('/payment', paymentRoutes);

// Basic route
app.get('/', (req, res) => {
    res.json({ message: 'PayOS Payment Integration Server' });
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
}); 