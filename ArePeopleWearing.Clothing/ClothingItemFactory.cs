using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Clothing
{
    public class ClothingItemFactory
    {
        public IClothingItem GetClothingItem(ClothingItemType clothingItemType)
        {
            switch(clothingItemType)
            {
                case ClothingItemType.Sundress:
                    return new Sundress();
                default:
                    throw new NotImplementedException("This ClothingItemType has not been registered in the factory");
            }
        }
    }
}
