-- CREATE TABLE: books
CREATE TABLE IF NOT EXISTS books (
    book_id INT PRIMARY KEY,
    title VARCHAR(255),
    authors TEXT,
    isbn VARCHAR(255),
    language_code VARCHAR(10),
    average_rating DECIMAL(3,2),
    ratings_count INT DEFAULT 0,
    original_publication_year INT,
    books_count INT,
    work_text_reviews_count INT
);

-- INDEX for performance on year
-- You must manually drop index if it already exists
CREATE INDEX idx_books_year ON books(original_publication_year);

-- TRIGGER: Delete all ratings when book is deleted
DELIMITER //
DROP TRIGGER IF EXISTS trg_delete_book_ratings;
CREATE TRIGGER trg_delete_book_ratings
AFTER DELETE ON books
FOR EACH ROW
BEGIN
    DELETE FROM ratings WHERE book_id = OLD.book_id;
END;
//
DELIMITER ;
