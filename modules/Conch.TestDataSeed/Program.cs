using Conch.Account;
using Conch.Goods;
using Conch.Stock;
using Dapr.Client;

const string storeName = "statestore";
const string key = "counter";

var daprClient = new DaprClientBuilder().Build();
var counter = await daprClient.GetStateAsync<int>(storeName, key);

while (true)
{
    Console.WriteLine($"Counter = {counter++}");

    await daprClient.SaveStateAsync(storeName, key, counter);
    await Task.Delay(1000);
}

var _dbcontext = new AccountDbContext();
if (_dbcontext.AccountMaster.Count() < 1)
{
    _dbcontext.AccountMaster.Add(new AccountMaster()
    {
        CreateTime = DateTime.Now.ToUniversalTime(),
        Name = "张三"
    });
    _dbcontext.SaveChanges();
}

var _goodsDBContext = new GoodsDBContext();
if (_goodsDBContext.GoodsMaster.Count() < 1)
{
    _goodsDBContext.GoodsMaster.Add(new GoodsMaster()
    {
        CreateTime = DateTime.Now.ToUniversalTime(),
        Name = "测试商品1",
    });
    _goodsDBContext.GoodsMaster.Add(new GoodsMaster()
    {
        CreateTime = DateTime.Now.ToUniversalTime(),
        Name = "测试商品2",
    });
    _goodsDBContext.GoodsMaster.Add(new GoodsMaster()
    {
        CreateTime = DateTime.Now.ToUniversalTime(),
        Name = "测试商品3",
    });
    _goodsDBContext.SaveChanges();
}

var _stockDBContext = new StockDBContext();
var goodsLists = _stockDBContext.StockGoods.ToList();
if (_stockDBContext.StockMaster.Count() < 1)
{
    var stock1 = _stockDBContext.StockMaster.Add(new StockMaster()
    {
        CreateTime = DateTime.Now.ToUniversalTime(),
        Name = "测试仓库A"
    });
    var stock2 = _stockDBContext.StockMaster.Add(new StockMaster()
    {
        CreateTime = DateTime.Now.ToUniversalTime(),
        Name = "测试仓库B"
    });

    var goodsList = _goodsDBContext.GoodsMaster.ToList();

    _stockDBContext.StockGoods.Add(new StockGoods()
    {
        StockId = stock1.Entity.Id,
        GoodsId = goodsList.FirstOrDefault().Id,
        GoodsName = goodsList.FirstOrDefault().Name,
        Num = 100
    });
    _stockDBContext.StockGoods.Add(new StockGoods()
    {
        StockId = stock2.Entity.Id,
        GoodsId = goodsList.LastOrDefault().Id,
        GoodsName = goodsList.LastOrDefault().Name,
        Num = 200
    });
    _stockDBContext.SaveChanges();
}
var goods = _stockDBContext.StockGoods.ToList();

goods.ForEach(c => c.FreezeNum = 0);
_stockDBContext.SaveChanges();

Console.WriteLine("DataSeed Finish");
Console.ReadLine();