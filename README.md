# Denuncia.Do

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
1. Each Denounce it's a Proposal Record on the database with a ProposalTypeId == 1, so, the Model Structure need to be checked on the Domain Before use a different Naming Convention
2. Only a Registered Owner and Logged Use, Can Edit or Delete a Proposal, only if the Status is not In Revision by Legislators StatusId==3.
3. For Anonimous Denounces We Need to Take Imei or Device ID.
--------------------------------
* List Of Denounces (Comming From API)
    * Main Image, Title, Resume

* Details Of Chossen Denounce (Comming From API)
    * Title, Description (Resume), Detail (Legal Mark), Votes Up, Votes Down

* Create Denounce
    * Anonimous Denounce 
      * Title, Description (Resume), Detail (Legal Mark), DeviceId
    * Registered User Denounce
      * Title, Description (Resume), Detail (Legal Mark), UserId, DeviceId

* Edit Denounce
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

--------------------------------
--------------------------------
