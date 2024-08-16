# UberTrash - Your Uber Ride to a Clean City (Application) 
When I came across the Steller build program, it was like a moment of eureka; for a long, I have always tried to find a solution to the unending waste problem facing Africa, andi had no clear-cut idea on how to solve it. 

First. 

Africa is at a pivotal point; in 2023, the waste crisis in Africa reached alarming proportions, with a staggering number of over 2.01 billion tons of household waste annually. This points to a global crisis as the population is ever-growing. 

Shockingly, 70-80% of this waste is recyclable, presenting a significant opportunity for sustainable waste management practices. However, the reality paints a dire picture: only **4%** of the waste is currently recycled in Africa, with 96% of it disposed of unlawfully, according to the World Health Organization.

While food waste can be organically disposed of,  Plastics are especially problematic. If not collected and managed properly, they contaminate and affect waterways and ecosystems for hundreds, if not thousands, of years. [In 2016, the world generated 242 million tonnes of plastic waste, or 12 percent of all solid waste](https://www.worldbank.org/en/news/press-release/2018/09/20/global-waste-to-grow-by-70-percent-by-2050-unless-urgent-action-is-taken-world-bank-report#), according to the report.

Another report released in 2022 by [UNEP](https://www.unep.org/) report revealed that Africa generates 17 million tonnes of plastic waste annually. Of this, a mere 12% is recycled, while 70% is mismanaged, ending up in landfills or the environment.

These issues led me and a few friends to develop an innovative solution that allows everyone to be part of the change to build a sustainable world. 

**The Growing Waste Crisis in Africa: Ubertrash to the Rescue.**

Ubertrash is an innovative application that allows **users to sell their plastic waste with just a button click. Our** idea focuses on getting everyone involved in building the eco-system. 

Sounds crazy, right? Let's dive into what we built. 


## Structure Design 


- [Interact with our App:](http://ubertrash-001-site1.ftempurl.com/) 


- [GitHub Repo](https://github.com/Tegaaumu/Uber-Trash/tree/830f02e18ce41eca385f9413b054b0333efe1bfc/Uber_Trash_Project)


- [Figma FIle](https://github.com/Tegaaumu/Uber-Trash/tree/830f02e18ce41eca385f9413b054b0333efe1bfc/Uber_Trash_Project) 


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723819637762_Screenshot+2024-08-16+at+15.47.11.png)


First, once a user logs into `[Ubertrash.com](http://ubertrash-001-site1.ftempurl.com/)`, they are asked to connect their wallet. we integrated the Solana test network, which would give the user  

![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723822823449_Screenshot+2024-08-16+at+16.40.18.png)


If you are a first time user, you can generate your wallet instantly and paste the address on the box below. 


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723822957153_Screenshot+2024-08-16+at+16.42.30.png)


Once you are done generating, you put your wallet address into the box and then reload the page; this will immediately connect, and then you can find your address at the top right corner of the page. 


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723823100382_Screenshot+2024-08-16+at+16.44.55.png)


As a first-timer, you are then asked You are offered two choices. **Order for a PickUp** or **Become a Pickup Agent**; let us decide to become a Pickup Agent and see how to interact with our application. 

## Become a PickUp Rider 
![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723633118189_Screenshot+2024-08-14+at+11.58.32.png)


To become a pickup rider, you must complete your KYC by registering your account and fill your details, and once you have that setup, you will be able to see pickup messages from users. 


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723823513222_Screenshot+2024-08-16+at+16.51.48.png)


After registration, you then have to confirm your email, which a code will be sent to you, once confirm you can then reload the page, and you are now an agent


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723823806284_Screenshot+2024-08-16+at+16.56.40.png)


Once you are logged in, scroll down to see your dashboard, and you will have access to all the users who have ordered for pickup; you can then select the one you are interested in picking.  


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723823924603_Screenshot+2024-08-16+at+16.58.36.png)


Once a rider accepts a pickup, he becomes that user's manager and can contact the user to arrange pickup. 

![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723824673317_Screenshot+2024-08-16+at+17.11.07.png)


Here are a few users who have registered to sell their plastic waste within an area; as a rider, you can contact the user and schedule a pick-up time, which should be within the next 6 hours, or another agent can accept the pickup.

Once the rider accepts to pick up, it shows processing on the user end, as the user would immediately see the pickup agent assigned to him and the time of arrival. 

Now, we have three different acceptance processes: **Processing, Accepting, and Receiving.**


- **Accepting:** This shows the transaction is still pending approval from the pickup agent to pick up the plastics from the user. 
- **Accepting:** This shows the pickup agent has accepted the cans from the user and has initiated a transfer of Solana coins to the user.
- **Received**: The user has been paid 


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723633287407_Screenshot+2024-08-14+at+12.01.21.png)


A user must meet a threshold of cans to drop off before an agent can pick it up, and only when the rider approves would the token be sent to the user via the Solana wallet connected above. 

![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723633554033_Screenshot+2024-08-14+at+12.05.47.png)


The rider must accumulate a certain threshold of plastic cans before dropping them back at the terminal, where they will be recycled and sold, and more money will be deployed into the eco-system, thereby keeping the environment clean and safe for our future generation while generating revenue. 


## To Use the Platform - User

To use the platform, you only need to connect your wallet and create an account to keep track of how many cans you have sold and also for our reward program, where we aim to give the highest plastic can transactions of the week or month tokens or NFTs to boast a competitive spirit while solving real-world problems,

First, the user must connect their wallet, as this would aid in easy disbursements of funds into their accounts because once the pickup approves the transaction, the token is sent to the user account.  


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723631939570_Screenshot+2024-08-14+at+11.38.51.png)


Second, the user must allocate the amount of plastic they intend to sell, as they must reach a threshold before ordering for pickup. Our minimum amount is 500 plastic bottles, which is equivalent to $50 in Solana tokens. 


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723828155877_Screenshot+2024-08-16+at+18.09.11.png)


Once a ride has been approved, the user will see the transaction as pending on his dashboard and awaiting pickup/payment confirmation from the pickup agent. 


![](https://paper-attachments.dropboxusercontent.com/s_F939401165CE2CCC120626D43EE69607CDB1545780C0970148C0A2BE4BD71270_1723828182962_Screenshot+2024-08-16+at+18.09.38.png)


An agent has 6 hours after accepting the pickup to complete the whole process of pickup to payment, or anyone else can accept the rider again. 

Since no agent has accepted the pickup, it is showing not available. Once an agent accepts the offer, it shows on the user dashboard, and a call or a follow-up notification of the pickup time. 

After the pickup, the payment status changes to processing and then paid. 


## Future Plans and Rollout 

Our plan is to include rewards and programs to allow users and agents to benefit more from the platform; here are our future plans.

**Clean and earn -** This program would allow users to to make significant environmental cleanup and then earn a token for doing something unique or above normal. This program would be every quarter, allowing everyone to vote for the best project. 

**Top Plastic Waste Picker** - we hope to organise the top sellers of the week with prices and NFTs to show how committed to the environment they are. 

**UberTrash Reach Out -** A reach-out program where we organize events to teach and train people on the havoc caused by waste and why being part of the UberTrash team is a plus for the ecosystem.

**UberTrash Ambassador -** A limited program where a limited number of people are given ambassador roles to reach out and create communities and train others to keep their environment clean. 

## Our Aim 

We aim to build a fast, transparent, and easy avenue from waste to wealth in Africa


## Our Objective 

Our objective is as follows 


- reduce the global carbon footprint by 
- Create jobs for over 400+ riders and over 20000 platform users in Nigeria and Africa.
- Build an ecosystem where plastic waste recycling is cheap and easy to implement in African zones.

UberTrash aims to build an ecosystem of young people passionate about the environment and work hard to keep it clean.

With the Solana eco-system, we aim to build a learn-to-reward where we set up a task that has to do with cleaning the environment and getting a reward for doing it. 

This would, in turn, create a momentum that popularises the Solana network in rural/urban areas as our focus is within Africa. 


## Conclusion 

Ubertrash is an innovation we believe is the future to begin a campaign on how recycling can be easy and fun to do. 

**We believe if you can book it, we can pick it up.** 

If you have any feedback or questions, please leave a comment below, as we are open to answering all questions. 





