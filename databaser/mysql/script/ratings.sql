-- CREATE TABLE: ratings (partitioned by user_id)
CREATE TABLE IF NOT EXISTS ratings (
    book_id INT NOT NULL,
    user_id VARCHAR(50) NOT NULL,
    rating INT,
    PRIMARY KEY (user_id, book_id)
)
PARTITION BY KEY(user_id)
PARTITIONS 6;

-- TRIGGER: After insert, update books.ratings_count (+1)
DELIMITER //
DROP TRIGGER IF EXISTS trg_rating_insert;
CREATE TRIGGER trg_rating_insert
AFTER INSERT ON ratings
FOR EACH ROW
BEGIN
    UPDATE books SET ratings_count = ratings_count + 1
    WHERE book_id = NEW.book_id;
END;
//
DELIMITER ;

-- TRIGGER: After delete, update books.ratings_count (-1)
DELIMITER //
DROP TRIGGER IF EXISTS trg_rating_delete;
CREATE TRIGGER trg_rating_delete
AFTER DELETE ON ratings
FOR EACH ROW
BEGIN
    UPDATE books SET ratings_count = ratings_count - 1
    WHERE book_id = OLD.book_id;
END;
//
DELIMITER ;
