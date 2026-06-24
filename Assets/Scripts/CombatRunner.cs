using UnityEngine;

public class CombatRunner : MonoBehaviour
{

    class Encounter
    {
        public Monster[] creatures;
        public Encounter()
        {
            Monster quick = new Monster("quick guy", 5,10,true);
            Monster slow = new Monster("slow guy", 10,5,true);
            Monster big = new Monster("big guy", 10,7,true);
            creatures = new Monster[] {quick, slow, big};
        }
        public void describe()
        {
            Console.WriteLine("Row 1: ");
        }
        public void checkSpeed()
        {
            for (int i = 0; i < creatures.Length - 1; i++)
            {
                if (creatures[i].getSpeed()<creatures[i+1].getSpeed())
                {
                    Monster Temp = creatures[i+1];
                    creatures[i+1]=creatures[i];
                    creatures[i]=Temp;
                }
            }
            
        }
        public void movePhase()
        {
            checkSpeed();
            for (int i = 0; i < creatures.Length; i++)
            {
                creatures[i].movePrompt();
            }
            describe();
            fightPhase();
        }
        public void fightPhase()
        {
            /*
            for (int i = 0; i < creatures.Length; i++)
            {
                creatures[i].fightPrompt();
                checkSpeed();
            }
            */
            describe();
            movePhase();
        }


    }
    class Monster
    {
        public string name;
        public int health;
        public int speed;
        public bool ally;
        public int position;
        private string[] moveList;

        public Monster(string n, int HP, int SP,bool al)
        {
            name = n;
            health = HP;
            speed = SP;
            ally = al;
            if (ally)
            {
                position = 1;
            }
            else
            {
                position = 4;
            }
        }
        public void movePrompt()
        {
            if (position != 1 || position != 4)
            {
                Console.WriteLine("1. Move forward \n 2. Stay \n 3. Move Back");
                string choice = Console.ReadLine();
                if (choice.Equals("1"))
                {
                    position+=1;
                }
                else if (choice.Equals("3"))
                {
                    position-=1;
                }
                else
                {
                    position+=0;
                }
            }
            else if (position == 1)
            {
                Console.WriteLine("1. Move forward \n 2. Stay");
                string choice = Console.ReadLine();
                if (choice.Equals("1"))
                {
                    position+=1;
                }
                else
                {
                    position+=0;
                }
            }
            else if (position == 4)
            {
                Console.WriteLine("1. Move Back \n 2. Stay");
                string choice = Console.ReadLine();
                if (choice.Equals("1"))
                {
                    position-=1;
                }
                else
                {
                    position+=0;
                }
            }
        }
        public void fightPrompt()
        {
            
        }
        public int getSpeed()
        {
            return speed;
        }
        public int getHP()
        {
            return health;
        }


    }



    void Start()
    {
        Encounter a = new Encounter();
        a.movePhase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
