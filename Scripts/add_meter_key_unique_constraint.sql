-- Ensures duplicate key submissions (same AK8, EK8, AK32, EK32) are overwritten
-- instead of creating duplicate rows. Run this if you see duplicate entries in the grid.
-- The MeterKeyRepository.SaveRows uses ON DUPLICATE KEY UPDATE which requires this index.

-- Drop old constraint if it existed (msn-based)
-- ALTER TABLE meter_key_rows DROP INDEX ux_meter_unique;

-- Add unique constraint on keys (safe to run - will error if index already exists)
ALTER TABLE meter_key_rows ADD UNIQUE INDEX ux_keys_unique (ak8, ek8, ak32, ek32);
