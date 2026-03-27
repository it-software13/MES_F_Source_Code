SELECT 
BASE007M.material_no AS '品号',
BASE007M.material_name AS '品名',
BASE007M.material_specifications AS '规格',
BASE007M.material_no AS '条码',
WMS003A1.storage_plan AS 'BN_NO'
FROM BASE007M(NOLOCK) JOIN WMS003A1(NOLOCK) ON BASE007M.material_no=WMS003A1.material_no
