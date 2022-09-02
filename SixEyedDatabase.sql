use [Sixeyed.Disposable.Database]

go

create table BookFeed
(
	Id int not null identity(1, 1) PRIMARY KEY ,
	Path varchar(540),
	LineCount int,
	WordCount int,
	ProcessingMilliseconds bigint
	)

create table BookLine 
(
	BookFeedId int,
	LineNumber int,
	WordCount int,
	Excerpt nvarchar(max)
	)
go
ALTER TABLE BookLine ADD CONSTRAINT FK_BookLine_BookFeed FOREIGN KEY (BookFeedId)
REFERENCES BookFeed(Id)