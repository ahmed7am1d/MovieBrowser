using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using MovieApp.Models;

namespace MovieApp.Services
{
    public static class CarouselViewServices
    {
        #region Methods
        public static ObservableCollection<WalkThrough> GetItemsForCarousel()
        {
            ObservableCollection<WalkThrough> walkThroughs = new ObservableCollection<WalkThrough>()
            {
                new WalkThrough
                {
                    Heading= "Game Of Thrones",
                    Caption="Game of Thrones is an American fantasy drama television series created by David Benioff and D. B. Weiss for HBO. It is an adaptation of A Song of Ice and Fire, a series of fantasy novels by George R. R. Martin, the first of which is A Game of Thrones.",
                    Image ="GOT.jpg"
                },

                new WalkThrough
                {
                    Heading = "Saw",
                    Caption ="Saw is a 2004 American horror film directed by James Wan (in his feature directorial debut) and written by Leigh Whannell from a story by Wan and Whannell. It is the first installment in the Saw film series, and stars Whannell, Cary Elwes, Danny Glover, Monica Potter, Michael Emerson, Ken Leung, and Tobin Bell.",
                    Image="SAW.jpg",
                },

                new WalkThrough
                {
                    Heading = "Friends",
                    Caption ="Friends is an American television sitcom, created by David Crane and Marta Kauffman, which aired on NBC from September 22, 1994, to May 6, 2004, lasting ten seasons.With an ensemble cast starring Jennifer Aniston, Courteney Cox, Lisa Kudrow, Matt LeBlanc, Matthew Perry and David Schwimmer, the show revolves around six friends in their 20s and 30s who live in Manhattan, New York City. ",
                    Image="Friends.jpg",
                }
            };

            return walkThroughs;
        }
        #endregion

    }
}
