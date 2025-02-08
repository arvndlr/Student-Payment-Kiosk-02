# Student Payment Kiosk


## Overview
The **Student Payment Kiosk** is a Windows Forms application designed to facilitate cashless transactions for educational institutions. It integrates **NFC technology** and **third-party payment solutions** (e.g., PayMongo) to allow students to conveniently pay their fees via an automated kiosk system. The system supports multiple roles, including **admin, cashier, and student**, and securely processes transactions using **PostgreSQL** as the database.

## Features
- **Student Authentication:** NFC card scanning for quick and secure login.
- **Admin Dashboard:** User management, transaction logs, and system configurations.
- **Cashier Module:** Manual transaction processing and report generation.
- **Automated Payments:** Integration with **PayMongo** for online transactions.
- **Coin & Bill Acceptor:** Accepts cash payments for those who prefer offline transactions.
- **Real-time Database Updates:** PostgreSQL ensures accurate and up-to-date records.
- **User-friendly UI:** Windows Forms-based interface for smooth navigation.

## Tech Stack
- **Frontend:** Windows Forms (.NET Framework)
- **Backend:** C# (.NET Framework)
- **Database:** PostgreSQL
- **Hardware Integration:** NFC Reader, Coin & Bill Acceptor
- **Payment Gateway:** PayMongo API

## Installation & Setup
### Prerequisites
- **.NET Framework** installed on your system
- **PostgreSQL** configured with the required database schema
- **PayMongo API Key** (for online payments)
- **NFC Reader** and **Cash Payment Hardware** connected

### Steps
1. Clone the repository:
   ```sh
   git clone https://github.com/your-username/student-payment-kiosk.git
   cd student-payment-kiosk
   ```
2. Open the solution in **Visual Studio**.
3. Set up the PostgreSQL database:
   - Run the provided SQL script to create tables.
4. Configure the `appsettings.json` file:
   ```json
   {
     "Database": {
       "ConnectionString": "Host=your_host;Database=your_db;Username=your_user;Password=your_password"
     },
     "PayMongo": {
       "APIKey": "your_paymongo_api_key"
     }
   }
   ```
5. Build and run the project in Visual Studio.

## Usage
- **Students:** Tap NFC card → View balance → Choose payment method → Confirm transaction.
- **Cashiers:** Log in → Process student payments → Generate reports.
- **Admins:** Manage users → Monitor transactions → Configure settings.

## Database Schema
(Provide an ERD or a brief table structure overview.)

## Contributing
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch (`feature-branch-name`).
3. Commit your changes and push.
4. Open a Pull Request for review.

## License
This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.

## Contact
For questions or collaboration, reach out to:
- **Arvin M. Delos Reyes** – [LinkedIn](#) | [Email](#)
- Open an **Issue** on this repository.

---

Give it a star ⭐ if you find this project useful!

