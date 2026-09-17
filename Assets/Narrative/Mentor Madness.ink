VAR AdityaChara = "Aditya"
VAR MitzliCHara = "Mitzli"
VAR SacinteChara = "Sacinte"
VAR MitzliHouseResident1 = "Customer 1"
VAR MitzliHouseResident2 = "Customer 2"
VAR AdultMTownie1 = "Neighbor 1"
VAR AdultFTownie1 = "Neighbor 2"
VAR LilKidTownie1 = "Little Girl"
VAR LilKidTownie2 = "Little Boy"
VAR FTeenTownie1 = "Teen Girl"
VAR FTeenTownie2 = "Teen Girl 2"
VAR MTeenTownie1 = "Teen Boy"
VAR MTeenTownie2 = "Teen Boy 2"
VAR Fish = "Weird Fish"
VAR Mushroom = "A Fungai?"
VAR HumanBaby = "Someone's Baby"
VAR WhiteThing = "???"
VAR YellowThing = "???"
VAR Collectible1 = "Lavender"
VAR Collectible2 = "Fabric Strips"
VAR Collectible3 = "Golden Oil"
VAR Collectible4 = "Haint Tears"
VAR DevTeam = "Dev Team"


===Customer1=== //Customer 1 Sprite//
It's been a while Adi. Yer folks well?  #Speaker: Customer 1

-> DONE

===Customer2=== //Customer 2 Sprite//
I can't believe my eyes. Aditya? Erasmus' little girl right?  #Speaker: Customer 2

'Mnot little anymore, sir. #SpeakerAditya
-> DONE


===OverworldTownie1=== //Little Boy who is also the requested sick kid//
cough cough #Speaker: Little Boy
Have you seen the grey fog? #Speaker: Little Boy
It's at the edge of town! #Speaker: Little Boy
It was white before, but now it's like the stories. Y'know, when the gods fight? #Speaker: Little Boy

Gods fighting? Didjyer auntie run out of bedtime stories and hafta pull a tale outta old Anean legends? #Speaker: Aditya

cough cough  #Speaker: Little Boy 
It's real! It's on the horizon now! #Speaker: Little Boy
cough, cough. hack #Speaker: Little Boy
-> DONE

===OverworldTownie2=== //Male Teen 1//
If this bus takes any longer, I'll be late. #Speaker: Teen Boy 1
Oh! Hello! #Speaker: Teen Boy 1
I'm Surprised to see a lady like you, way out here. #Speaker: Teen Boy 1
Pardon me ma'am but I need to keep an eye out for the bus and practice my lines. I scored a big interview. #Speaker: Teen Boy 1
You mean that bus over there? #Speaker: Aditya
Huh?! What bus?! -'Scuse me! #Speaker: Teen Boy 1
-> DONE


===OverworldTownie3=== //Adult Male Townie/
These new clothes are so itchy... I can't gett'm dirty. #Speaker: Neighbor 1
-> DONE

===OverworldTownie4=== //Little Girl//
I'm gonna catch a Sulfasail in the tidepools after high tide! #Speaker: Little Girl
-> DONE

===OverworldTownie5=== //Female Teen 1 aka girl with the watering can//
No matter how much I weed and weed, my tomatoes get smothered by this sticky stuff and withers away. This plot is doomed. #Speaker: Female Teen 1
-> DONE

===OverworldTownie6=== //Adult Female Townie/
No rabbits in the traps, no fish in the river... We'll hafta start buying our food from those factory men in a minute, too. #Speaker: Neighbor 2

//alt line that would get repeated as the final line in the dialogue interaction.//
It's fine I'll do it myself. Capture it by the throat..  cut it myself. I can't afford to wait for someone to rescue me. #Speaker: Neighbor 2
-> DONE

===OverworldTownie7=== //Male Teen 2//
Is it east? No... north from here..... #Speaker: Teen Boy 2

H-Hey! Do you know the fastest way to get to the heart of the forest? I need meat before sunset. #Speaker: Teen Boy 2

I think it's- #Speaker:Aditya

No, wait! Nevermind I can figure it out... I'll never get it if I keep asking for help. #Speaker: Teen Boy 2
-> DONE

===OverworldTownie8=== //Female Teen 2//
Blue bells, cockle shells, #Speaker: Teen Girl
evie, ivy oooooover....
-> DONE

===Lavender===

Lavendar Sprig

Scientific Name: Lavandula angustifolia

A sprig from a small, grey-green, shrub that dons long stalks, Its plump flowers secrete a soothing oil when squeezed.
-> DONE

===FabricScraps===
Fabric Scraps

Made from tightly woven cotton fibers. Its jagged edges suggests it was torn from a larger bolt or discarded.
-> DONE

===GoldenOil===

Golden Oil

Viscous oil that glitters when appearing under direct light.

-> DONE

===HaintTears=== //Collectible 4 (Healing Items that can be scattered about the level)//
Haint Tears

Opalescent fruit that abundantly hangs from the Whisperia tree. Commonly called, Haint Tears, this fruit references the long  
-> DONE

===WeirdFish===
Gurbple....

Gup Gup!

gwuaaaah.

//All options that player can cycle through one after the other.//
-> DONE

===WhiteCreature===
Raaaahhh

-> DONE

===YellowCreature===
huuuussss husssssss...
auegh...
-> DONE

===Fungai===
muuuh? #Speaker: Mushroom

A Pneuma?! Here? Are you an actual Pneuma? #Speaker: Aditya

hup hup hup? #Speaker: Mushroom 

-> DONE

===ABaby===
Where's your mama? #Speaker: Aditya
waaaaaaaaaaahhhhh

reaaaaaahhhh reaaaaaahhhh

hurgggh
-> DONE


===PostBusExploration===
Where is everyone?  #Speaker:Aditya
-> DONE

===POI1===
What a strange tree... #SpeakerAditya
-> DONE

===POI2===
Was the water always this murky? #SpeakerAditya
-> DONE

===POI3===
Poor fawn. How long have you been left here to rot? #Speaker:Aditya
-> DONE

===MitzliHouse===

*[Object 1] 
A small black box. The key seems to be broken off inside of the lock, keeping it's contents sealed shut. #Speaker:Aditya
    **[Keep Looking] ->MitzliHouse
    **[Talk to Mitzli] ->TalkToMitzli
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

* Flighty as ever I see. -> Next
* I got in a few minutes ago... -> Next

=== Next ===
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
*[Yes] -> ExitHouse
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
-> DONE

===POI4===
Thats a rock. Never seen a rock before? #Speaker: Dev Team
-> DONE

===POI5===
Thats a tree... You don't get out much do you? #Speaker: Dev Team
-> DONE

===POI6===
Wow! A tree, but in green. Exhilerating. #Speaker: Dev Team
-> DONE

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