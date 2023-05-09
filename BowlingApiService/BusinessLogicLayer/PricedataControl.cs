using BowlingApiService.DTOs;
using BowlingData.DatabaseLayer;
using ShModel;

namespace BowlingApiService.BusinessLogicLayer
{
    public class PricedataControl : IPriceData
    {
        private readonly IPriceAccess _priceAccess;
        public PricedataControl(IPriceAccess inPriceAccess)
        {
            _priceAccess = inPriceAccess;
        }

        public int Add(PriceDto newPrice)
        {
            int insertedId = 0;
            try
            {
                Price? foundPrice = ModelConversion.PriceDtoConvert.ToPrice(newPrice);
                if (foundPrice != null)
                {
                    insertedId = _priceAccess.CreatePrice(foundPrice);
                }
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

        public bool Delete(int id)
        {
            try
            {
                bool isDeleted = _priceAccess.DeletePriceById(id);
                return isDeleted;
            }
            catch
            {
                return false;
            };
        }

        public PriceDto? Get(int id)
        {
            PriceDto? foundPriceDto;
            try
            {
                Price? foundPrice = _priceAccess.GetPriceById(id);
                foundPriceDto = ModelConversion.PriceDtoConvert.FromPrice(foundPrice);
            }
            catch
            {
                foundPriceDto = null;
            }
            return foundPriceDto;
        }

        public List<PriceDto>? Get()
        {
            List<PriceDto>? foundDtos;
            try
            {
                List<Price>? foundPrices = _priceAccess.GetAllPrices();
                foundDtos = ModelConversion.PriceDtoConvert.FromPriceCollection(foundPrices);
            }
            catch
            {
                foundDtos = null;
            }
            return foundDtos;
        }

        public bool Put(PriceDto priceToUpdate)
        {
            try
            {
                Price? updatedPrice = ModelConversion.PriceDtoConvert.ToPrice(priceToUpdate);
                return _priceAccess.UpdatePrice(updatedPrice);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
