IF DB_ID('demoDb') IS NOT NULL
  set noexec on               -- prevent creation when already exists

CREATE DATABASE demoDb;
GO