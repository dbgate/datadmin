CREATE TABLE d2dx_folder (
    id INTEGER NOT NULL PRIMARY KEY,
    parent_id INTEGER NULL,
    folder_name VARCHAR(100) NOT NULL
)

GO

CREATE TABLE d2dx_file (
    id INTEGER NOT NULL PRIMARY KEY,
    folder_id INTEGER NOT NULL,
    file_name VARCHAR(100) NOT NULL,
    data_type VARCHAR(10) NULL,
    file_data TEXT NULL
)

GO

INSERT INTO d2dx_folder (id, folder_name) VALUES (1, 'diagrams')

GO

INSERT INTO d2dx_folder (id, folder_name) VALUES (2, 'sqlscripts')

GO

UPDATE d2dx_info SET par_value='2' WHERE par_name='dbversion'
