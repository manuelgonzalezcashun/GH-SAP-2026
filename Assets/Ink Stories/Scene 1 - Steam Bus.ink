VAR AdityaChara = "Aditya"
VAR MitzliCHara = "Mitzli"
VAR TrainConductor = "Train Conductor"
VAR BNpc1 = "BNpc1"
VAR BNpc2 = "BNpc2"
VAR BNpc3 = "BNpc3"
VAR LookOutside = "Look Outside"



-> LetterSection

===LetterSection===
My Dearest Aditya,

    I wish I were penning this missive to you under better means. Forgive my haste in foregoing the usual pleasantries, but I must I request your attendance immediately. I fear I cannot navigate this burgeoning predicament unaided. 
    Fare thee better than I fare.  

    You're very loving friend,  

    Dr. Mitzli Arzu  #Speaker:Mitzli


This is not the homecoming I had anticipated. I hope I can actually be of use to him. #Speaker:Aditya


-> InteractionTest
===InteractionTest===

Press 'E' to interact

*[Bus Passenger 1] 
An utterly unremarkable, plain-faced gentleman. He appears weary... I trust he shall feel better with time. #Speaker:Aditya
    **[Keep Looking] ->InteractionTest
    **[Done Looking] ->ProgressionCheckPoint
*[Bus Passenger 2] 
With such fashionable hair and rouge, it's no marvel that such a beauty has that gentleman quite captivated. #Speaker:Aditya
    **[Keep Looking] ->InteractionTest
    **[Done Looking] ->ProgressionCheckPoint
*[Bus Passenger 3] 
He appears... quite advanced in his years for such a young woman. I've never comprehended the inclination for gentlemen of advanced age to be captivated by their junior counterparts. #Speaker:Aditya
    **[Keep Looking] ->InteractionTest
    **[Done Looking] ->ProgressionCheckPoint
*[Look Outside]
The smog is growing thicker. We'll soon be compelled to wear masks out in the countryside as well. #Speaker:Aditya
    **[Keep Looking] ->InteractionTest


* ->ProgressionCheckPoint
===ProgressionCheckPoint===
Done looking around?
*[Yes]
-> LeaveBus
*[No]
What's that over there? #Speaker:Aditya
-> InteractionTest


-> LeaveBus
===LeaveBus===
The bus chugged to life, hissing hot steam from it's nostrils.   
Guests disembarking for Anea, please prepare for the next junction. #Speaker:TrainConductor
Ready or not... #Speaker:Aditya

-> END