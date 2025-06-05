-- CREATE TABLE: users (partitioned by country)
CREATE TABLE IF NOT EXISTS users (
    user_id VARCHAR(50),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    country VARCHAR(100),
    antal INT,
    PRIMARY KEY (user_id, country)
)
PARTITION BY LIST COLUMNS (country) (
    PARTITION p_dk VALUES IN ('Denmark'),
    PARTITION p_se VALUES IN ('Sweden'),
    PARTITION p_no VALUES IN ('Norway'),
    PARTITION p_fi VALUES IN ('Finland'),
    PARTITION p_de VALUES IN ('Germany'),
    PARTITION p_uk VALUES IN ('England'),
    PARTITION p_others VALUES IN (
        'France', 'Italy', 'Spain', 'Greece', 'Belgium', 'Poland',
        'Switzerland', 'Portugal', 'Netherlands', 'Hungary', 'Ireland', 'USA', 'Czech Republic', 'Austria'
    )
);

-- TRIGGER: Delete all ratings when user is deleted
DELIMITER //
DROP TRIGGER IF EXISTS trg_delete_user_ratings;
CREATE TRIGGER trg_delete_user_ratings
AFTER DELETE ON users
FOR EACH ROW
BEGIN
    DELETE FROM ratings WHERE user_id = OLD.user_id;
END;
//
DELIMITER ;
