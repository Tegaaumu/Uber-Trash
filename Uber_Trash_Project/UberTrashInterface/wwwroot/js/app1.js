document.addEventListener('DOMContentLoaded', () => {
    const StellarSdk = window.StellarSdk;
    const accountInfoElement = document.getElementById('accountInfoElement');
    const statusElementx = document.getElementById('statusElement');
    const outputDiv = document.getElementById('outputDiv');

    document.getElementById('createButton').addEventListener('click', async () => {
        const statusElement = document.getElementById('status');

        try {
            // Generate a new keypair
            const pair = StellarSdk.Keypair.random();

            // Send keys to C# method
            const response = await fetch('/api/Stellar/SaveKeys', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    PublicKey: pair.publicKey(),
                    SecretKey: pair.secret()
                })
            });

            if (response.ok) {
                statusElementx.textContent = 'Keys sent to server successfully. Store your key properly';
                accountInfoElement.textContent = 'Here is your public key' + pair.publicKey();
                outputDiv.textContent = 'Here is your secert key.' + pair.secret();
            } else {
                statusElement.textContent = 'Failed to send keys to server.';
            }
        } catch (error) {
            console.error('Error creating account:', error);
            statusElement.textContent = 'Error creating account. See console for details.';
        }
    });
});