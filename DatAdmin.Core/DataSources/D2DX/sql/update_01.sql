CREATE TABLE d2dx_info (
    par_name VARCHAR(50) NOT NULL PRIMARY KEY,
    par_value VARCHAR(50) NOT NULL
);

GO

INSERT INTO d2dx_info (par_name, par_value) VALUES ('dbversion', '1');
