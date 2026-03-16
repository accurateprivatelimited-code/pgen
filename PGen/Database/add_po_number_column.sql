-- Add PO Number column to meter_key_rows table
ALTER TABLE meter_key_rows 
ADD COLUMN po_number VARCHAR(50) NULL AFTER ek32;

-- Add index for better search performance on PO Number
CREATE INDEX idx_meter_key_rows_po_number ON meter_key_rows(po_number);

-- Update the comment/description for the table (optional)
-- ALTER TABLE meter_key_rows COMMENT = 'Meter key generation records with PO Number tracking';
