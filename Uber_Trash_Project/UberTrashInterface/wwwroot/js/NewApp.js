
    document.addEventListener('DOMContentLoaded', () => {
        document.getElementById('tegaEmma').addEventListener('click', async () => {
        const statusElement = document.getElementById('statusElement');
        const accountInfoElement = document.getElementById('accountInfoElement');
        const outputDiv = document.getElementById('outputDiv');
        const publicKey = document.getElementById('inputText').value;

        if (!publicKey) {
            statusElement.textContent = 'Public key is required.';
            return;
        }

        if (!StellarSdk.StrKey.isValidEd25519PublicKey(publicKey)) {
            statusElement.textContent = 'Invalid public key format.';
            accountInfoElement.innerHTML = '';
            return;
        }

        try {
            // Create a server instance
            const server = new StellarSdk.Server('https://horizon-testnet.stellar.org');

            // Load account details
            const account = await server.loadAccount(publicKey);
            statusElement.textContent = 'Connected to Stellar account: ' + publicKey;
            console.log('Account details:', account);

            // Display account balances
            accountInfoElement.innerHTML = 'Balances:<br>';
            account.balances.forEach(balance => {
                accountInfoElement.innerHTML += `Asset: ${balance.asset_type}, Balance: ${balance.balance}<br>`;
            });

            // Send public key to C# API
            const response = await fetch('/api/Stellar/Connect', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ PublicKey: publicKey })
            });

            if (response.ok) {
                outputDiv.innerText = 'Public key sent to server successfully: ' + publicKey;
            } else {
                const errorMessage = await response.text();
                outputDiv.innerText = 'Failed to send public key to server. Error: ' + errorMessage;
            }
        } catch (error) {
            console.error('Error connecting to Stellar:', error);
            statusElement.textContent = 'Error connecting to Stellar. See console for details.';

            if (error.response && error.response.status === 400) {
                statusElement.textContent = 'Bad request: Invalid public key or unfunded account.';
            } else {
                statusElement.textContent = 'An error occurred: ' + error.message;
            }
        }
    });
});

function submitText(event) {
    event.preventDefault(); // Prevent the form from submitting the traditional way
    const inputText = document.getElementById("inputText").value;
    document.getElementById("outputDiv").innerText = inputText;
}