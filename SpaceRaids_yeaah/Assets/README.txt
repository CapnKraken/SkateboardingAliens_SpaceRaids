Team Skateboarding Aliens

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
General Programming Practices
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

Variable names: camelCase with lowercase first letter.
Method names and Class names: CamelCase with uppercase first letter.

Keep code decoupled as much as possible.
If you need something to happen in one script as a result of something that happens in a different script, use the messsaging system.

Make sure to put you name at the top of all scipts you create.

Comment religously, make sure it is clear what everything does. 
Even if it seems obvious to you, comment anyway. Programmers can be extremely thick-headed at times (that includes future you).

Summaries are your friend. For example:

	/// <summary>
	/// Variable to keep on counting.
	/// </summary>
	int counter = 0;

	This makes it so when you hover over the variable name, it'll show the description of it.
	Make a summary by typing "///" above a variable or method declaration and it'll auto-fill the comment.
		It's especially helpful for methods as it lets you put descriptions for the method, return value, and parameters.

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
GitHub Practices
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

Everytime you are starting a new feature, make sure to do it in a new, separate branch. When that feature is finished, then you can push it to the main branch. 
	This isn't just for new features, but any change at all. Do not work directly on the main branch. The github app tells you at the top left-ish what the current branch is.
	Make sure that isn't "main."
If you make changes before creating your new branch, make sure to select the option to move the changes to your new branch when creating it.

Naming Convention for Branches:
"Your name_Feature Title"
	-if you're adding a bunch of little things, put "Housekeeping" or some other general term for the feature title, 
		and put the specific things in the individual commit descriptions.
	
Make sure to give a detailed description of what you changed/added. You don't have to push every time you commit, push once you are done with your coding session.

Once you are done implementing the feature you created the branch for, create a pull request. This will merge your branch into the main one.
	Also, one other team member must approve the pull request before it can be merged.

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Messaging System
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

ManagedObject:
-The script representing a gameObject which can send and receive messages through the messenger.
-Use it as the base class of any script in which you want to send or respond to messages.

-Don't use Unity's Start() and OnDestroy() methods in managed objects.
-Instead use the inherited methods Initialize() and DeInitialize(). They do the same thing.
	(the start and destroy methods are used in the parent object to handle some things with the game manager)

Notifications:
-can be sent using the Notify() method
-First parameter is the non-specific category of notification (e.g. UI, Audio, etc.)
	This is so objects will only listen for notifications that are relevant to them.
	Relevant objects can be specified in the inspector of any managed object.
	If you aren't sure which category to pick, use GENERAL. (all objects will receive this notification)
-Second parameter is the message itself.
-Other data can be incorporated into the message if necessary.
	For instance: "DamagePlayer 12". String.Split()[0] can be used to separate the message from the values also sent.

This system uses a GameManager object

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Input System
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=




=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

If you have any questions about any of this (or suggestions to add), post them in the discord.

If they're about the purpose of the messaging system specifically, this may help:
https://gameprogrammingpatterns.com/observer.html

Ignore all of the code samples on that site, because I implemented the pattern in a different way,
but that explains the problem the messaging system aims to solve pretty well I think.

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

Contributers:
Matthew
TJ


