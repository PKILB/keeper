CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- CREATE TABLE keeps(
--   id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
--   creatorId VARCHAR(255) NOT NULL,
--   name VARCHAR(100) NOT NULL,
--   description VARCHAR(1000) NOT NULL DEFAULT 'No Description',
--   img VARCHAR(200) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1525310072745-f49212b5ac6d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8Zmxvd2VyfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=700&q=60',
--   views INT NOT NULL DEFAULT 0,

--   FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
-- ) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(1000) NOT NULL DEFAULT 'No Description',
  img VARCHAR(200) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1606041008023-472dfb5e530f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8Zmxvd2VyfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=700&q=60',
  isPrivate BOOLEAN NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE vaults;
