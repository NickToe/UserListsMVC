# UserLists MVC
> MVC web application mainly designed to create user lists (followlists, wishlists and custom lists) and add items of different content types to them for tracking your own activity.


## Table of Contents
* [Technologies Used](#technologies-used)
* [General Info](#general-information)
* [Features](#features)
* [Build](#build)
* [Room for Improvement](#room-for-improvement)
* [Contact](#contact)


## Technologies Used
- ASP.NET Core MVC
- C# 10
- .NET 6
- EF Core 6
- PostgreSQL


## General Information
### I. User Lists
This application allows registered users to search for games and movies in API by title and save any found items to 3 types of user lists:
1. Followlists - stores general information (poster, id, title) about item and Notification column (bool)
to be able to receive notifications when there are new comments added for the followed item
2. Wishlists - stores general information (poster, id, title) about item, item status (none, planned, ongoing, finished), personal score (as a number) and vote (as like/dislike) and several dates:
   * Added Date: when item was added to the list
   * Planned Date: when user is planning to do some action on item (play a game, watch a movie, etc)
   * Finished Date: when that action is done
3. Custom lists: the same as wishlists but the user is able to create as many as they like with different names (for example: "Halloween movies", "Christmas movies")

### II. Detailed Item Page
The user may open the detailed information page about the item (information depends on content type of item) that is fetched from API by clicking on it in the search or any list. This page displays item information, likes/dislikes, comments, replies and view counter.

### III. Notifications
In-app notifications for the users will be generated in following cases:
1. User follows the item and new comment is added to it
2. User receives a reply to their comment 
3. Planned date for the item in user wishlists/custom lists is today


## Features
- User registration via email service
- Followlists (for receiving notifications)
- Wishlists (for tracking own activity)
- Custom lists (for tracking own activity on individual lists)
- Search item(s) by title (in API)
- Page with detailed information about item (from API) and view counter per page
- Comments and replies for individual item
- Likes and dislikes for individual item, comments and replies
- View lists of other users (if they are not private)
- User notifications


## Build
dotnet build --configuration Release


## Room for Improvement
- More work on frontend part (Views)
- More content types (Music, Books, etc)


## Contact
Created by [@N1ckToe](https://web.telegram.org/k/#@N1ckToe) - feel free to contact me!
