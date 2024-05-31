CREATE TABLE IF NOT EXISTS users (
    id INTEGER PRIMARY KEY,
    username VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    address VARCHAR(255),
    region VARCHAR(255),
    zip_code INTEGER,
    water_supply INTEGER,
    gas_supply INTEGER,
    light_supply INTEGER
);

CREATE TABLE IF NOT EXISTS sensor_readings (
    id INTEGER PRIMARY KEY,
    user_id INTEGER,
    sensor_type VARCHAR(255) CHECK(sensor_type IN ('movement', 'doors', 'humidity', 'temperature', 'luminosity', 'sound')),
    value REAL,
    room VARCHAR(255),
    hour TIMESTAMP,
    UNIQUE(user_id, sensor_type, room, hour),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS supplies (
    id INTEGER PRIMARY KEY,
    name VARCHAR(255) CHECK(name IN ('water', 'light', 'gas')),
    unit VARCHAR(10) CHECK(unit IN ('m3', 'kWh')),
    cost_unit REAL,
    type VARCHAR(10) CHECK(type IN ('fix', 'variable'))
);

CREATE TABLE IF NOT EXISTS consumption (
    id INTEGER PRIMARY KEY,
    date TIMESTAMP,
    supply_id INTEGER,
    value REAL,
    cost REAL,
    FOREIGN KEY (supply_id) REFERENCES supplies(id)
);

CREATE TABLE IF NOT EXISTS daily_consumption (
    id INTEGER PRIMARY KEY,
    date TIMESTAMP,
    supply_id INTEGER,
    value REAL,
    cost REAL,
    FOREIGN KEY (supply_id) REFERENCES supplies(id)
);

CREATE TABLE IF NOT EXISTS period (
    id INTEGER PRIMARY KEY,
    name VARCHAR(255) CHECK(name IN ('month', 'bimonthly', 'quarter', 'semester', 'annual')),
    initial_date TIMESTAMP,
    final_date TIMESTAMP
);

CREATE TABLE IF NOT EXISTS period_consumption (
    id INTEGER PRIMARY KEY,
    period_id INTEGER,
    supply_id INTEGER,
    total_value REAL,
    total_cost REAL,
    FOREIGN KEY (period_id) REFERENCES period(id),
    FOREIGN KEY (supply_id) REFERENCES supplies(id)
);

CREATE TABLE IF NOT EXISTS door_states (
    id INTEGER PRIMARY KEY,
    user_id INTEGER,
    room VARCHAR(255),
    door_name VARCHAR(255),
    state BOOLEAN,
    hour TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id)
);

-- Relaciones adicionales (definidas como claves foráneas en las tablas)
-- Ref: users.id - sensor_readings.user_id 
-- Ref: consumption.supply_id - supplies.id 
-- Ref: daily_consumption.supply_id - supplies.id 
-- Ref: period_consumption.supply_id - supplies.id 
-- Ref: period_consumption.period_id - period.id 
-- Ref: users.gas_supply - supplies.id 
-- Ref: users.light_supply - supplies.id 
-- Ref: users.water_supply - supplies.id 
