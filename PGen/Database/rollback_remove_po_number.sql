-- Rollback Script: Remove PO Number column from meter_key_rows table
-- Version: 1.0
-- Date: 2026-03-16

-- Remove the index first
DROP INDEX IF EXISTS idx_meter_key_rows_po_number ON meter_key_rows;

-- Remove the column
ALTER TABLE meter_key_rows DROP COLUMN IF EXISTS po_number;

-- Verify the changes
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = DATABASE() 
AND TABLE_NAME = 'meter_key_rows' 
AND COLUMN_NAME = 'po_number';

-- Show table structure for verification
DESCRIBE meter_key_rows;

SELECT 'Rollback completed successfully. PO Number column removed from meter_key_rows table.' as status;
