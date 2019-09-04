
CREATE TABLE 'Forecasts' (
    CityId INTEGER NOT NULL,
    TargetDate TEXT NOT NULL,
    Temperature INTEGER,
    WindSpeed INTEGER,
    WindDirection TEXT,
    Pressure INTEGER,
    Humidity INTEGER,
    PRIMARY KEY ('CityId', 'TargetDate')
);
