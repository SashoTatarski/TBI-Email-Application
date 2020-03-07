# Email Application Manager

### Link to Trello Board:  [LINK](https://trello.com/b/JEfDjhk8/tbi-final-project)

# The project
       Deliver a system capable to facilitate tracking, monitoring and processing customer loan applications
       coming to the bank via e-mail. 


## Functionalities
---

### Authentication

       User authentication is performed by ASP.NET Identity authentication. Users cannot register in the
       application but are created by another user with “Manager” rights. There are two types of roles -
       Manager and Operator
       // For initial login use the following credentials: manager@ems.com pass: Password-123 


### Loan Application Format

       No specific email format was defined so far – mail can be in any encoding, HTML or pure text, with
       or without attachments. Each email may contain a single loan application.

### Email Registration 

       Emails are fetched on regular intervals (1 min) from Gmail API. Gmail inbox and system database are
       always synchronized. As soon as email is read from Gmail API it is recorded in the system DB. All incoming
       emails are registered with unique ID automatically within the system. 


## Email/Application Status
---

 - Not reviewed – default status – all new emails are classified as such. 

 - Invalid application – all email which does not contain loan applications must be marked by operator in such status. Only employees with “Manager” rights can return the email in “Not reviewed” status.

-  New – all emails must be reviewed and only the one contains valid loan application will be set by employee with “New” status. 

- Open – status marked for under processing – in order to set to “Open” status, the system requests **customer name**, **customer ID/EGN** and **customer phone information**  to be fulfilled by the bank employee. 
Respective formal system control of the provided data is applied – client-side, server-side validation.

 - Closed - status of the email when the application is set approved or rejected

## Status Flow
---

Possible status transition depends on granted user/operator rights. They should be as follow:

- Operators
  - Not reviewed **-->** Invalid Application
  - Not reviewed **-->** New
  - New **-->** Open 
  - Open **-->** Closed (allowed only if the application was set in Open status by the same operator)

- Managers
  - Not reviewed **<-->** Invalid Application
  - Not reviewed **<-->** New
  - New **<-->** Open (regardless who put the application in Open status)
  - Open **<-->** Closed (regardless who put the application in Open status)
  - Closed **-->** New 

### Email/Application record

        Body of emails under “Not Review” status are omitted and not stored within the system DB. As soon as email is set with status New – the body of the email is recorded in DB as well.
        - Minimal set of application record
           - Sender 
           - Date received 
           - Subject
           - Body 
           - Count of attachments and total (sum) size of attachments (in Mb)
           - Customer unique ID / EGN – filled by bank employee / agent when change from New->Open status 
           - Customer contact phone – filled by bank employee / agent when change from New->Open status
           - Date/Time of initial registration within the system
           - Date/Time when the application was set in current status 
           - Date/Time when the application was set in terminal status (Closed or Invalid Application) 

### Personal Data Subject of GDPR

The following fields contain customer private data therefore are kept encrypted within the DB:
 - Sender 
 - Body
 - Customer unique ID / EGN

# Working screens
## 1. Email preview - preview the contents of the email
- Data provided:
   - All attachments are listed – (file name, file size). No download or preview of attachment is possible.
   - Mail body preview 

## 2. List **All** emails - the screen lists all incoming emails
- **Data provided**
   - DB ID
   - Email DateTime received
   - Sender
   - Subject
   - Status
   - Icon indicating if there any attachments
- **Email preview button** – showing email preview onClick
- **Actions**
   - All mails: option to mark specific email as not valid
   - For emails marked as “not valid” there is an option to remove the flag “not valid” (only for Managers role)
   - Emails marked as not valid are grayed out
   - Email list shall is obtained from mail server in every one minute

## 3. List **New** emails - the screen lists all emails with status new
- **Data provided**
   - DB ID
   - Email DateTime received
   - Sender
   - Subject
   - Status
   - Time since is in new status (order by this field desc)
- **Email preview button** – showing email preview onClick
- **Actions**
   - Option to assign application for work (change status to Open)
   - New fields for fulfillment - Customer name, ID/EGN and phone information
 
 ## 4. List **Open** emails/applications - the screen lists all emails with status new
- **Data provided**
   - DB ID
   - Email DateTime received
   - Sender
   - Subject
   - Status
   - Time since is in open status (order by this field desc)
   - User who put application in status “Open” status (only Manager role)
- **Email preview button** – showing email preview onClick
- **Actions**
   - Close application with Approve or Reject status
   - Return application status to “New” (only Manager role)
   - “Operator” role – can filter applications only opened by currently logged user
   - If logged user has Manager role – all application in Open status are shown and there is filter by User who put application in this status

 ## 5. List **Closed** emails/applications - the screen lists all emails with status closed
- **Data provided**
   - DB ID
   - Email DateTime received
   - Sender
   - Subject
   - Status (“approved” or “rejected”)
   - User who put application in status Closed (only Manager role)
- **Email preview button** – showing email preview onClick
- **Actions**
   - Return application status to “New” (only Manager role) 
   - Operator role – can filter applications only put in status “closed” by currently logged user
   - If logged user has Manager role – all application in Closed status are shown and there is filter by User who put application in this status



# Architecture
 - Web Application (MVC) + Business Layer + Data Layer

## Logging
 ### Serilog 
All actions (status changes) are logged with information set 
 - TimeStamp – Date
 - Action performed (status change, etc.)
 - User performed the action

## Database
 - Azure Database

 ## Backend
 - .NET CORE 2.2
 - Entity Framework Core

## Security 
- Secure data communication between all “layers” - HTTPs
- Data encryption for confidential customer data. 
- Resistant to attacks like cross-site scripting, SQL injection, buffer overflow, session hijacking etc.
   - Input validation performed on server and client side
   - Error messages displayed to user, customer or third parties do not provide sensitive information on data or system
