Team Skateboarding Aliens

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
General Programming Practices
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

Variable names: camelCase with lowercase first letter.
Method names and Class names: CamelCase with uppercase first letter.

Keep code decoupled as much as possible.
If you need something to happen in one script as a result of something that happens in a different script, use the messsaging system.


=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Messaging System
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

ManagedObject:
-The script representing a gameObject which can send and receive messages through the messenger.
-Use it as the parent of any script in which you want to send or respond to messages.

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

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

If you have any questions about any of this (or suggestions to add), post them in the discord.

If they're about the messaging system specifically, this may help:
https://gameprogrammingpatterns.com/observer.html

Ignore all of the code samples on that site, because I implemented the pattern in a different way,
but that explains the problem the messaging system aims to solve pretty well I think.

-Matthew


