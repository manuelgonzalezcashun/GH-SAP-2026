VAR AdityaChara = "Aditya"
VAR MitzliCHara = "Mitzli"
VAR BNpc1 = "Sacinte"
VAR ChildNPC = "ChildNPC"
VAR DevTeam = "Dev Team"


-> PostBusExploration

===PostBusExploration===

Where is everyone?  #Speaker:Aditya


===POI1===
What a strange tree... #SpeakerAditya

===POI2===
Was the water always this murky? #SpeakerAditya

===POI3===
Poor fawn. How long have you been left here to rot? #Speaker:Aditya

===MitzliHouse===

*[Object 1] 
A small black box. The key seems to be broken off inside of the lock, keeping it's contents sealed shut. #Speaker:Aditya
    **[Keep Looking] ->MitzliHouse
    **[Talk to Mitzli] ->ProgressionCheckPoint
*[Object 2] 
A simple handmade toy. You recognize the clothes it's wearing though it's face has long since faded. #Speaker:Aditya
    **[Keep Looking] ->MitzliHouse
    **[Talk to Mitzli] ->TalkToMitzli
*[Object 3 3] 
A bottle of glimmering, unidentified pink liquid. It smells vaguely of rose petals. #Speaker:Aditya
    **[Keep Looking] ->MitzliHouse
    **[Talk To Mitzli] ->TalkToMitzli



* ->TalkToMitzli
===TalkToMitzli===
Aditya! I didn't hear you come in. When did you get back? #Speaker:Mitzli

*[Flighty as ever I see.]
*[I got in a few minutes ago...]

We have much to discuss. #Speaker:Mitzli

Tell me everything. #Speaker:Aditya

1 hour later.

That's all I know so far. #Speaker:Mitzli
What can I do to help? #Speaker:Aditya
There's not much light left in the day. Follow me.
If we can get to Forest Edge Name while there's still daylight, you can look at the state of things yourself. We can even maybe collect some samples to test back here. #Speaker:Mitzli
Should I bring anything? #Speaker:Aditya
Besides yourself and a suspension of disbelief? No.
Let me know when you're ready and I'll take the lead.

Are you ready to leave?
*[Yes]
-> ExitHouse
*[No]
What's that over there? #Speaker:Aditya
->MitzliHouse


-> ExitHouse
===ExitHouse===
You could barely get a foot out the door when something small barrels into you.

Ow! #Speaker:ChildNPC 

I'm sorry– #Speaker:Aditya

Dr. Mitzli, you gotta come quick! #Speaker:ChildNPC

Mitzli helpeed the little girl up, dusting off her dirtied smock. 

What ails you, Name? #Speaker:Mitzli

Not me! It's Townie1! Some monster attacked her house while Townie2 was away. We gotta help them! Cmon, lets go! NOW! #Speaker: ChildNPC

Slow down. What did the monster look like? #Speaker: Mitzli

Does it matter– #Speaker Aditya

I wouldn't bother asking if it didn't. What did it look like, Name? #Speaker:Mitzli

I-I don't know. A black mist? I only saw it for a second after Townie1 screamed. I came to you as quick as I could. #Speaker:ChildNPC

Mitzli muttered under his breath.

Black mist... seen at dusk...

Aditya! Go find Sacinte and ask her for these ingredients (Recipe1)! Meet me back at Townie1's house, quickly. #Speaker:Mitzli

Of course. #Speaker:Aditya

->OpenWorldExploration

===OpenWorldExploration===

Where was Mrs.Sacinte's house again? #Speaker:Aditya

===POI4===
Thats a rock. Never seen a rock before? #Speaker: Dev Team


===POI5===

Thats a tree... You don't get out much do you? #Speaker: Dev Team

===POI6===
Wow! A tree, but in green. Exhilerating. #Speaker: Dev Team


===SacinteHouse===

You enter a house rich in the scent of damp wood and warm spices. Steam is wafting off of a small pot kept hot by a small fire.

Mrs? Mrs. Sacinte are you in? It's urgent! #Speaker:Aditya

There is a creaking upstairs.

Is that there John and Irma's little girl I hear down there? Certainly not. #Speaker:Sacinte

A surprisingly short woman for the voice paired with her, rounds the corner as she descends downstairs.

It is ma'am, how are yo- #Speaker:Aditya

Ooooooh weee! I ain't seen you since you were- what? Fifteen-Sixteen? Now look atcha. Still a whole lotta trouble packed in ya, little lady? #Speaker:Sacinte

Aditya felt warm all over, like she had made herself at home in Sacinte's soup pot.

You betcha– Say I hate to interrupt our reunion, but Mitzli sent me over with very important duties. Something is up with Townie1. #Speaker:Aditya

Child, something is always up with Townie1... #Speaker:Sacinte

No, for real this time. Some little girl came up to us and said there was a monster attacking their house. #Speaker:Aditya

Monster? We don't have monsters here. Only Pneuma and forest beasts. #Speaker:Sacinte

*[I can find out is origin later!]
*[Monster of the alchemic variety it sounds like...]

I won't keep you then. What do you need. #Speaker:Sacinte

These ingredients. #Speaker:Aditya

Makeing yourself Recipe1? That will only work if it is a pneuma affected with StatusEffectName. #Speaker:Sacinte

Seems like that's a risk Mitzli is willing to take. #Speaker:Aditya

That boy. Feet facing front, head on backwards. If he wasn't so hard headed, we wouldn't be in this mess as is... #Speaker:Sacinte

What is going on in Anea lately? #Speaker:Aditya

Sacinte went to her cabinet and riffled through it as she talked, mostly to herself. 


Let me hush. No use in speaking on conjecture.

Here girl, is this what you need? #Speaker:Sacinte

You've obtained Ingredient 1, Ingredient 2, and Ingredient 3. You can now make Recipe1!

Yes, this is perfect, thank you! #Speaker:Aditya

Now remember, these things aren't much help on their own.You gotta combine them to get the EFFECT you want from it, ya' hear? #Speaker:Sacinte

Yes ma'am. Combine Ingredient 1, with Ingredient 2 and Ingredient 3 to get Recipe1. #Speaker:Aditya

Good. Now hurry along, go help our friends. #Speaker:Sacinte

-> END 