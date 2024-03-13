# Denuncia.Do.

This project born from the need from people to have a way of communication between municipalities and communities.

Some municipalities, have their platforms, but they are complex to validate the veracity of complaints.

Denounced, was born with the purpose of offering a free platform to these municipalities to get connected with their representatives, with the purpose, not only to improve communication between them, but also to improve joint work for faster attention and problem solving.

# Vision.
Be the communication link between municipes and representatives to make complaints and promote proposals for state improvements, which can be voted, validated and/or supported by other municipes, thus creating a representative democratic portal where each individual can contribute ideas and solutions to the improvement of its locality.

# Architecture
Denounced consists of three main modules developed with Microsoft technologies, using the .Net Framework and Xamarin for its development:

1. Back End Web Project:

Module of administration of the complaints, by the employees of the town councils.

In this tool, the employees of the city council receive, validate, report and close the complaints, after being served.

2. Web Portal Client:

It consists of a web project, so that the community make their complaints, in the same, the users of the service create a profile, must specify when making their complaint, evidence to support this.

Through the portal, they can see the complaints of other community members, follow it, give their opinion or provide possible solutions or more evidence.

3. Mobile Project:

It has the same functionalities as the web portal, with the addition, that the automatic location can be sent, from the cell phone.

# Need to Be Done
--------------------------------
Movil Client
--------------------------------
Notes
1. Each Denounce it's a Proposal Record on the database with a ProposalTypeId == 1, so, the Model Structure need to be checked on the Domain Before use a different Naming Convention.
2. Only a Registered Owner and Logged Use, Can Edit or Delete a Proposal, only if the Status is not In Revision by Legislators StatusId==3.
3. For Anonimous Denounces We Need to Take Imei or Device ID.
--------------------------------

* Api Service: A Communication Service based on General POCO Entities to comunicate the client with the Backend.
* Helpers Service: Are the utilities used to get decentralized job and use generics ways to get jobs done, like UploadPhotoService, ValidateService, etc.
* Device Service: Service to work with sencible information of the device.
* DataService: Service to manipulated database based on POCO entities, can be done on sQLite or another.
* Translate Service: Our App need to be internazionalized ready, so, we need a service to translate labels and text is its possible.

* List Of Denounces: This is the **Main Page** where the list of each recent denounce is displayed, this screen use the ApiService to Get a list of the Proposals (Denounces), and save the data to a local database (This is Optional)
    * ListView: Main Image, Title, Resume.
    * Choosen 1: Filter By Proposal Type.
    * Choosen 2: Filter By Status.
    * Choosen 3: Order By Name, Proposal Type, Date, Status.
    * Search Bar: Type text and Filter by Name, Proposal Type, Date, Status.
    * Pluss Icon: Icon to Make a new Denounce.
    * Loggin Option: Can be ignored if we create a Menu
    * My Info Option: Can be ignored if we create a Menu
    
* Details Of Chossen Denounce: We come here after clic on a denounce on the main page, it need to go to the api and update the record on the local database (optional) 
    * View: Title, Description (Resume), Detail (Legal Mark), Owner.
    * Vote Up Icon: Only displayed if i'm a logged user.
    * Vote Down Icon: Only displayed if i'm a logged user.
    * Edit Suggestion: A Logged User can suggest a modification, than can be approved by the admin or the owner.
    * Report Icon: Only displayed if i'm a logged user.

* Create Denounce: This it's the section to create a denounce than can be created as an anonimous or as a register user
    * Pop Up: If im not logged, display a Message to indicated than my denounce will be created as a anonimous denounced, and remembering than those kind of denounces can't be update or deleted and two buttons: "Login" or "Continue Anonimous"
    * Anonimous Denounce 
      * View: Title, Description (Resume), Detail (Legal Mark).
      * Behind: DeviceId.
    * Registered User Denounce
      * View: Title, Description (Resume), Detail (Legal Mark).
      * Behind: UserId, DeviceId.

* Edit Denounce
    * Title, Description (Resume), Detail (Legal Mark), UserId, DenounceId
    
* Edit Denounce Suggestions
    * Title, Description (Resume), Detail (Legal Mark), UserId, DenounceId

* Delete Denounce
    * UserId, DenounceId

* Login
    * Email, Password

* Register
    * Email, Name, Lastname, Rnc, Phone, Address, Password, DeviceId

* Vote Up
    * DenounceId, UserId, DeviceId

* Vote Down
    * DenounceId, UserId, DeviceId

* Report Denounce
    * DenounceId, UserId, Reason, DeviceId
    
* User Details Page

* Edit User Details Page

* Down Solicitation
  This page its used for the user to make a solisitation to the admins to get removed from the plataform
  
 

--------------------------------
--------------------------------
