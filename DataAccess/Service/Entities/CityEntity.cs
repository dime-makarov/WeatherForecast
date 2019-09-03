using NPoco;

namespace Dm.WeatherForecast.DataAccess.Service.Entities
{
    [TableName("Cities")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class CityEntity
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
