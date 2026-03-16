-- Migration Script: Add PO Number support to meter_key_rows table
-- Version: 1.0
-- Date: 2026-03-16

-- Check if column already exists to avoid errors
SET @column_exists = (
    SELECT COUNT(*) 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_SCHEMA = DATABASE() 
    AND TABLE_NAME = 'meter_key_rows' 
    AND COLUMN_NAME = 'po_number'
);

-- Only add column if it doesn't exist
SET @sql = IF(@column_exists = 0, 
    'ALTER TABLE meter_key_rows ADD COLUMN po_number VARCHAR(50) NULL AFTER ek32',
    'SELECT ''Column po_number already exists'' as message'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Add index for better search performance (only if column was added or index doesn't exist)
SET @index_exists = (
    SELECT COUNT(*) 
    FROM INFORMATION_SCHEMA.STATISTICS 
    WHERE TABLE_SCHEMA = DATABASE() 
    AND TABLE_NAME = 'meter_key_rows' 
    AND INDEX_NAME = 'idx_meter_key_rows_po_number'
);

SET @sql = IF(@index_exists = 0, 
    'CREATE INDEX idx_meter_key_rows_po_number ON meter_key_rows(po_number)',
    'SELECT ''Index idx_meter_key_rows_po_number already exists'' as message'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Verify the changes
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT,
    ORDINAL_POSITION
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = DATABASE() 
AND TABLE_NAME = 'meter_key_rows' 
AND COLUMN_NAME = 'po_number';

-- Show table structure for verification
DESCRIBE meter_key_rows;

-- Test the new column with a sample insert (commented out - uncomment for testing)
-- INSERT INTO meter_key_rows (session_id, msn, meter_type, model, set_index, ak8, ek8, ak32, ek32, po_number) 
-- VALUES ('test-session', '12345678', 'Test Type', 'Test Model', 1, '12345678', '87654321', '12345678901234567890123456789012', '21098765432109876543210987654321', 'PO-12345');

SELECT 'Migration completed successfully. PO Number column added to meter_key_rows table.' as status;
