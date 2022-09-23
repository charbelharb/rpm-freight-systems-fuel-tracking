MERGE FuelPrices pd USING (SELECT @PriceDate,@Price) AS src(PriceDate, price)
      ON (pd.priceDate = src.PriceDate)
 WHEN MATCHED THEN
      UPDATE SET price = @Price
 WHEN NOT MATCHED THEN
      INSERT (priceDate, price)
      VALUES (@PriceDate,@Price);