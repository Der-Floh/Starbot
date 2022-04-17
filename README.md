# Starbot
A Discordbot to automatically read and save the I.Rule suggestions made in the StarBox server

## Current functions of Starbot:

### Help Commands:
```
!help-baby  |  Shows you what you can do with Baby suggestions
```
```
!help-item  |  Shows you what you can do with Item suggestions
```
```
!help-item-active  |  Shows you what you can do with Active Item suggestions
```
```
!help-enemy  |  Shows you what you can do with Enemy suggestions
```
```
!help-translation  |  Shows you what you can do with translations
```
```
!help-wiki  |  Shows you what you can do with the wiki integration
```
```
!help-github  |  Shows you which github links are available
```
### 1. Manage Suggestion for Babies, Items, Active Items, Enemies (Included rating system and getting top rated ideas and stuff)
#### New Ideas:
```
!idea-baby  |  Adds a new Baby idea by using the following syntax:
Name= text 
Cost= number 
HP= number 
Type= text 
Damage= number 
Firerate= number 
Recharge= number 
Abilities= text
```
```
!idea-item  |  Adds a new Item idea by using the following syntax:
Name= text
Cost= number
Tier= number
Description= text
Effect= text
```
```
!idea-item-active  |  Adds a new Active Item idea by using the following syntax:
Name= text
Description= text
Effect= text
```
```
!idea-enemy  |  Adds a new Enemy idea by using the following syntax:
Name= text
HP= number
Type= text
Damage= number
Firerate= number
Walkspeed= number
Abilities= text
Appearance= text
```
#### Getting Ideas:
```
!get-baby  |  Gets a Baby idea by entering the babies id or name:
!get-baby id/name
```
```
!get-item  |  Gets an Item idea by entering the item id or name:
!get-item id/name
```
```
!get-item-active  |  Gets an Active Item idea by entering the items id or name:
!get-item-active id/name
```
```
!get-enemy  |  Gets an Enemy idea by entering the enemies id or name:
!get-enemy id/name
```
#### Getting Top Rated Ideas:
```
!get-top-baby  |  Gets the best rated Baby ideas by entering the number of best babies:
!get-top-baby 10  ->  Gets the top 10 best rated babies
```
```
!get-top-item  |  Gets the best rated item ideas by entering the number of best items:
!get-top-item 10  ->  Gets the top 10 best rated items
```
```
!get-top-item-active  |  Gets the best rated active item ideas by entering the number of best active items:
!get-top-item-active 10  ->  Gets the top 10 best rated active items
```
```
!get-top-enemy  |  Gets the best rated enemy ideas by entering the number of best enemies:
!get-top-enemy 10  ->  Gets the top 10 best rated enemies
```

## README will get finished soon
#### 2. Sending Babies, Items, Active Items, Enemies which are currently in the game
#### 3. Sending translation files for all added Translations (Currently: ru, en, fr, cn, es, de)
#### 4. Sending a list of all translators of I.Rule
#### 5. Sending links to itch homepage, important github pages (like ruler and translations) and wiki pages
