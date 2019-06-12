drop table RelativeRank.dbo.user_to_show_mapping;
drop table RelativeRank.dbo.users;
drop table RelativeRank.dbo.shows;


create table RelativeRank.dbo.users
(
	username varchar(32) primary key,
	password varchar(128)
)

create table RelativeRank.dbo.shows
(
	name varchar(128) primary key
)

create table RelativeRank.dbo.user_to_show_mapping
(
	username varchar(32) foreign key references RelativeRank.dbo.users(username),
	showname varchar(128) foreign key references RelativeRank.dbo.shows(name),
	[rank] smallint not null,
	constraint pk_user_to_show_mapping primary key clustered (username, showname),
	constraint uq_rank_unique_to_user unique(username, [rank])
)

insert into RelativeRank.dbo.users (username, password) values ('MP7373', 'password');

insert into RelativeRank.dbo.shows (name) values ('Angel Beats!');
insert into RelativeRank.dbo.shows (name) values ('Fairy Tail: Final Series');
insert into RelativeRank.dbo.shows (name) values ('One Punch Man nd Season');
insert into RelativeRank.dbo.shows (name) values ('Sewayaki Kitsune no Senko san');
insert into RelativeRank.dbo.shows (name) values ('Shingeki no Kyojin Season Part');
insert into RelativeRank.dbo.shows (name) values ('Tate no Yuusha no Nariagari');
insert into RelativeRank.dbo.shows (name) values ('Akame ga Kill!');
insert into RelativeRank.dbo.shows (name) values ('Aldnoah Zero');
insert into RelativeRank.dbo.shows (name) values ('Aldnoah Zero nd Season');
insert into RelativeRank.dbo.shows (name) values ('Anitore! EX');
insert into RelativeRank.dbo.shows (name) values ('Anitore! XX: Hitotsu Yane no Shita de');
insert into RelativeRank.dbo.shows (name) values ('Another');
insert into RelativeRank.dbo.shows (name) values ('Bakemonogatari');
insert into RelativeRank.dbo.shows (name) values ('Baoh Raihousha');
insert into RelativeRank.dbo.shows (name) values ('Blend S');
insert into RelativeRank.dbo.shows (name) values ('Boku dake ga Inai Machi');
insert into RelativeRank.dbo.shows (name) values ('Boku no Hero Academia');
insert into RelativeRank.dbo.shows (name) values ('Boku no Hero Academia nd Season');
insert into RelativeRank.dbo.shows (name) values ('Boku no Hero Academia rd Season');
insert into RelativeRank.dbo.shows (name) values ('Bokusatsu Tenshi Dokuro chan');
insert into RelativeRank.dbo.shows (name) values ('Charlotte');
insert into RelativeRank.dbo.shows (name) values ('Citrus');
insert into RelativeRank.dbo.shows (name) values ('Clannad');
insert into RelativeRank.dbo.shows (name) values ('Clannad: After Story');
insert into RelativeRank.dbo.shows (name) values ('Claymore');
insert into RelativeRank.dbo.shows (name) values ('Code Geass: Hangyaku no Lelouch');
insert into RelativeRank.dbo.shows (name) values ('Code Geass: Hangyaku no Lelouch R');
insert into RelativeRank.dbo.shows (name) values ('Comic Girls');
insert into RelativeRank.dbo.shows (name) values ('Cowboy Bebop');
insert into RelativeRank.dbo.shows (name) values ('Darling in the FranXX');
insert into RelativeRank.dbo.shows (name) values ('Deadman Wonderland');
insert into RelativeRank.dbo.shows (name) values ('Death Note');
insert into RelativeRank.dbo.shows (name) values ('Domestic na Kanojo');
insert into RelativeRank.dbo.shows (name) values ('Ebiten: Kouritsu Ebisugawa Koukou Tenmonbu');
insert into RelativeRank.dbo.shows (name) values ('Elfen Lied');
insert into RelativeRank.dbo.shows (name) values ('Eromanga sensei');
insert into RelativeRank.dbo.shows (name) values ('Fairy Tail');
insert into RelativeRank.dbo.shows (name) values ('Fate /stay night: Unlimited Blade Works');
insert into RelativeRank.dbo.shows (name) values ('Fate /stay night: Unlimited Blade Works nd Season');
insert into RelativeRank.dbo.shows (name) values ('Fate /Zero');
insert into RelativeRank.dbo.shows (name) values ('Fate /Zero nd Season');
insert into RelativeRank.dbo.shows (name) values ('FLCL');
insert into RelativeRank.dbo.shows (name) values ('Fullmetal Alchemist: Brotherhood');
insert into RelativeRank.dbo.shows (name) values ('Gakusen Toshi Asterisk');
insert into RelativeRank.dbo.shows (name) values ('Gamers!');
insert into RelativeRank.dbo.shows (name) values ('Gate: Jieitai Kanochi nite Kaku Tatakaeri');
insert into RelativeRank.dbo.shows (name) values ('God Eater');
insert into RelativeRank.dbo.shows (name) values ('Harukana Receive');
insert into RelativeRank.dbo.shows (name) values ('Hibike! Euphonium');
insert into RelativeRank.dbo.shows (name) values ('Highschool of the Dead');
insert into RelativeRank.dbo.shows (name) values ('Hinako Note');
insert into RelativeRank.dbo.shows (name) values ('Jitsu wa Watashi wa');
insert into RelativeRank.dbo.shows (name) values ('Joukamachi no Dandelion');
insert into RelativeRank.dbo.shows (name) values ('K On!');
insert into RelativeRank.dbo.shows (name) values ('K On!!');
insert into RelativeRank.dbo.shows (name) values ('Kakegurui');
insert into RelativeRank.dbo.shows (name) values ('Kaze no Tani no Nausica u e');
insert into RelativeRank.dbo.shows (name) values ('Keijo!!!!!!!!');
insert into RelativeRank.dbo.shows (name) values ('Kemono Friends');
insert into RelativeRank.dbo.shows (name) values ('Kill la Kill');
insert into RelativeRank.dbo.shows (name) values ('Kimi no Na wa');
insert into RelativeRank.dbo.shows (name) values ('Kiss x Sis TV');
insert into RelativeRank.dbo.shows (name) values ('Kobayashi san Chi no Maid Dragon');
insert into RelativeRank.dbo.shows (name) values ('Koe no Katachi');
insert into RelativeRank.dbo.shows (name) values ('Kono Subarashii Sekai ni Shukufuku wo!');
insert into RelativeRank.dbo.shows (name) values ('Koutetsujou no Kabaneri');
insert into RelativeRank.dbo.shows (name) values ('Kuzu no Honkai');
insert into RelativeRank.dbo.shows (name) values ('Love Live! School Idol Project');
insert into RelativeRank.dbo.shows (name) values ('Love Live! School Idol Project nd Season');
insert into RelativeRank.dbo.shows (name) values ('Love Live! Sunshine!!');
insert into RelativeRank.dbo.shows (name) values ('Love Live! Sunshine!! nd Season');
insert into RelativeRank.dbo.shows (name) values ('Love Live! The School Idol Movie');
insert into RelativeRank.dbo.shows (name) values ('Lucky u Star');
insert into RelativeRank.dbo.shows (name) values ('Made in Abyss');
insert into RelativeRank.dbo.shows (name) values ('Mahou Shoujo Madoka u Magica');
insert into RelativeRank.dbo.shows (name) values ('Mahoutsukai no Yome');
insert into RelativeRank.dbo.shows (name) values ('Mahoutsukai no Yome: Hoshi Matsu Hito');
insert into RelativeRank.dbo.shows (name) values ('Majo no Takkyuubin');
insert into RelativeRank.dbo.shows (name) values ('Masamune kun no Revenge');
insert into RelativeRank.dbo.shows (name) values ('Mononoke Hime');
insert into RelativeRank.dbo.shows (name) values ('Monster Musume no Iru Nichijou');
insert into RelativeRank.dbo.shows (name) values ('Musaigen no Phantom World');
insert into RelativeRank.dbo.shows (name) values ('Neon Genesis Evangelion');
insert into RelativeRank.dbo.shows (name) values ('Neon Genesis Evangelion: The End of Evangelion');
insert into RelativeRank.dbo.shows (name) values ('NHK ni Youkoso!');
insert into RelativeRank.dbo.shows (name) values ('Nisekoi');
insert into RelativeRank.dbo.shows (name) values ('Nisekoi:');
insert into RelativeRank.dbo.shows (name) values ('No Game No Life');
insert into RelativeRank.dbo.shows (name) values ('One Punch Man');
insert into RelativeRank.dbo.shows (name) values ('Orange');
insert into RelativeRank.dbo.shows (name) values ('Pan de Peace!');
insert into RelativeRank.dbo.shows (name) values ('Papa no Iukoto wo Kikinasai!');
insert into RelativeRank.dbo.shows (name) values ('Perfect Blue');
insert into RelativeRank.dbo.shows (name) values ('Ping Pong the Animation');
insert into RelativeRank.dbo.shows (name) values ('Pokemon Movie : Kimi ni Kimeta!');
insert into RelativeRank.dbo.shows (name) values ('Pokemon Movie : Minna no Monogatari');
insert into RelativeRank.dbo.shows (name) values ('Princess Principal');
insert into RelativeRank.dbo.shows (name) values ('Pupa');
insert into RelativeRank.dbo.shows (name) values ('Re:Zero kara Hajimeru Isekai Seikatsu');
insert into RelativeRank.dbo.shows (name) values ('Rokka no Yuusha');
insert into RelativeRank.dbo.shows (name) values ('Saenai Heroine no Sodatekata');
insert into RelativeRank.dbo.shows (name) values ('Saenai Heroine no Sodatekata u d');
insert into RelativeRank.dbo.shows (name) values ('Sakura Trick');
insert into RelativeRank.dbo.shows (name) values ('Sen to Chihiro no Kamikakushi');
insert into RelativeRank.dbo.shows (name) values ('Serial Experiments Lain');
insert into RelativeRank.dbo.shows (name) values ('Shigatsu wa Kimi no Uso');
insert into RelativeRank.dbo.shows (name) values ('Shingeki no Kyojin');
insert into RelativeRank.dbo.shows (name) values ('Shingeki no Kyojin Season');
insert into RelativeRank.dbo.shows (name) values ('Shirobako');
insert into RelativeRank.dbo.shows (name) values ('Shokugeki no Souma');
insert into RelativeRank.dbo.shows (name) values ('SoniAni: Super Sonico The Animation');
insert into RelativeRank.dbo.shows (name) values ('Steins;Gate');
insert into RelativeRank.dbo.shows (name) values ('Suzumiya Haruhi no Yuuutsu');
insert into RelativeRank.dbo.shows (name) values ('Sword Art Online');
insert into RelativeRank.dbo.shows (name) values ('Sword Art Online Alternative: Gun Gale Online');
insert into RelativeRank.dbo.shows (name) values ('Sword Art Online II');
insert into RelativeRank.dbo.shows (name) values ('Tengen Toppa Gurren Lagann');
insert into RelativeRank.dbo.shows (name) values ('Terra Formars');
insert into RelativeRank.dbo.shows (name) values ('Toaru Kagaku no Railgun');
insert into RelativeRank.dbo.shows (name) values ('Toaru Kagaku no Railgun S');
insert into RelativeRank.dbo.shows (name) values ('Toradora!');
insert into RelativeRank.dbo.shows (name) values ('Tsurezure Children');
insert into RelativeRank.dbo.shows (name) values ('Violet Evergarden');
insert into RelativeRank.dbo.shows (name) values ('Wakaba Girl');
insert into RelativeRank.dbo.shows (name) values ('Yakusoku no Neverland');
insert into RelativeRank.dbo.shows (name) values ('Yamada kun to nin no Majo');
insert into RelativeRank.dbo.shows (name) values ('Yojouhan Shinwa Taikei');
insert into RelativeRank.dbo.shows (name) values ('Yuru Yuri');
insert into RelativeRank.dbo.shows (name) values ('Yuru Yuri u a u a');
insert into RelativeRank.dbo.shows (name) values ('Zettai Shougeki: Platonic Heart');
insert into RelativeRank.dbo.shows (name) values ('Aho Girl');
insert into RelativeRank.dbo.shows (name) values ('Boku no Kanojo ga Majimesugiru Sho bitch na Ken');
insert into RelativeRank.dbo.shows (name) values ('Dororo');
insert into RelativeRank.dbo.shows (name) values ('Fate /Apocrypha');
insert into RelativeRank.dbo.shows (name) values ('Hajimete no Gal');
insert into RelativeRank.dbo.shows (name) values ('Imouto sae Ireba Ii');
insert into RelativeRank.dbo.shows (name) values ('Isekai Shokudou');
insert into RelativeRank.dbo.shows (name) values ('Katsugeki /Touken Ranbu');
insert into RelativeRank.dbo.shows (name) values ('Knight s Magic');
insert into RelativeRank.dbo.shows (name) values ('Koi to Uso');
insert into RelativeRank.dbo.shows (name) values ('Netsuzou TRap');
insert into RelativeRank.dbo.shows (name) values ('One Piece');
insert into RelativeRank.dbo.shows (name) values ('Shokugeki no Souma: Ni no Sara');
insert into RelativeRank.dbo.shows (name) values ('Shoujo u Kageki Revue Starlight');
insert into RelativeRank.dbo.shows (name) values ('Re:Zero kara Hajimeru Isekai Seikatsu nd Season');

insert into RelativeRank.dbo.user_to_show_mapping (username, showname, [rank]) values ('MP7373', 'Yuru Yuri', 1);

