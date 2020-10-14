using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();


            HashSet<UserNode> result = FriendsOfFriends(users[0], 2);

            foreach (UserNode member in result)
            {
                Console.WriteLine(member.ToString());
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }


        private static int GetDistance(UserNode root, UserNode searched)
        {
            int counter = 1;
            Queue<UserNode> queue = new Queue<UserNode>();
            HashSet<UserNode> set = new HashSet<UserNode>();

            queue.Enqueue(root);
            set.Add(root);

            while (queue.Count > 0)
            {
                UserNode user = queue.Dequeue();
                if(user.FirstName.Equals(searched.FirstName) && user.LastName.Equals(searched.LastName))
                {
                    return counter;
                }
                foreach (UserNode friend in user.Friends)
                {
                    if(!set.Contains(friend))
                    {
                        queue.Enqueue(friend);
                        set.Add(friend);
                        counter++;
                    }
                }
            }
            return 99999;
        }

        private static HashSet<UserNode> FriendsOfFriends(UserNode root, int distance)
        {
            HashSet<UserNode> result = new HashSet<UserNode>();

            Queue<UserNode> queue = new Queue<UserNode>();
            HashSet<UserNode> set = new HashSet<UserNode>();

            queue.Enqueue(root);
            set.Add(root);

            int depth = 0;

            while(queue.Count > 0 && depth != distance)
            {
                int numberOfFriends = queue.Count;

                while(numberOfFriends > 0)
                {
                    UserNode currentUser = queue.Dequeue();

                    foreach (UserNode friend in currentUser.Friends)
                    {
                        if (!set.Contains(friend) && !result.Contains(friend))
                        {
                            queue.Enqueue(friend);
                            set.Add(friend);
                            result.Add(friend);
                        }
                    }

                    numberOfFriends--;
                }

                depth++;        
            }

            return result;
        }
    }
}
