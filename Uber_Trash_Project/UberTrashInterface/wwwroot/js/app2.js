document.addEventListener('DOMContentLoaded', () => {
    const StellarSdk = window.StellarSdk;

    document.getElementById('createButton').addEventListener('click', async () => {
        //const statusElement = document.getElementById('status');
        const statusElement = document.getElementById('statusElement');
        const accountInfoElement = document.getElementById('accountInfoElement');

        try {
            // Generate a new keypair
            const pair = StellarSdk.Keypair.random();

            // Fund the account using Friendbot (testnet only)
            const fundResponse = await fetch(`https://friendbot.stellar.org/?addr=${pair.publicKey()}`);
            if (!fundResponse.ok) {
                throw new Error('Failed to fund account');
            }

            // Connect to the Stellar account
            const server = new StellarSdk.Server('https://horizon-testnet.stellar.org');
            const account = await server.loadAccount(pair.publicKey());
            statusElement.textContent = 'Connected to Stellar account: ' + pair.publicKey();
            console.log('Account details:', account);

            // Display account balances
            let balanceString = 'Balances:<br>';
            account.balances.forEach(balance => {
                //balanceString += `Asset: ${balance.asset_type}, Balance: ${balance.balance}<br>`;
                balanceString += balance.balance;
            });
            accountInfoElement.innerHTML = balanceString;

            // Send keys and balance to C# method
            const response = await fetch('/api/Stellar/SaveKeys', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    PublicKey: pair.publicKey(),
                    SecretKey: pair.secret(),
                    Balance: balanceString
                })
            });

            if (response.ok) {
                statusElement.textContent += ' Keys and balance sent to server successfully.';
            } else {
                statusElement.textContent += ' Failed to send keys and balance to server.';
            }
        } catch (error) {
            console.error('Error creating account:', error);
            statusElement.textContent = 'Error creating account. See console for details.';
        }
    });


    
});