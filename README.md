# Starbot
A Discordbot for the Starbox server to manage suggestions and support the community with many useful functions

## Current functions of Starbot:

### Help Commands:
```
!i-help  |  Shows you the other help commands
```
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
!get-baby id/name  ->  where id is the discord message id 
```
```
!get-item  |  Gets an Item idea by entering the item id or name:
!get-item id/name  ->  where id is the discord message id 
```
```
!get-item-active  |  Gets an Active Item idea by entering the items id or name:
!get-item-active id/name  ->  where id is the discord message id 
```
```
!get-enemy  |  Gets an Enemy idea by entering the enemies id or name:
!get-enemy id/name  ->  where id is the discord message id 
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

### 2. Sending Babies, Items, Active Items, Enemies which are currently in the game
```
!get-existing-baby  |  Gets an existing Baby by entering the babies id or name:
!get-existing-baby id/name
```
```
!get-existing-item  |  Gets an existing Item by entering the items id or name:
!get-existing-item id/name
```
```
!get-existing-item-active  |  Gets an existing Active Item by entering the items id or name:
!get-existing-item-active id/name
```
```
!get-existing-enemy  |  Gets an existing Enemy by entering the enemies id or name:
!get-existing-enemy id/name
```

### 3. Sending translation files for all added Translations (Currently: ru, en, fr, cn, es, de)
```
!get-translation  |  Gets the translation of language entered in shortform:
!get-translation en
```

### 4. Sending a list of all translators of I.Rule
```
!get-translators  |  Gets all translators of I.Rule:
!get-translators
```

### 5. Sending links to itch homepage and important github pages (like ruler and translations)
#### Itch.io Homepage:
```
!itch-irule  |  Sends a link to the Itch.io homepage of I.Rule:
!itch-irule
```
#### Github pages:
```
!github-releases  |  Sends a link to the Github Releases page of I.Rule:
!github-releases
```
```
!github-ruler  |  Sends a link to the Github Ruler page of I.Rule:
!github-ruler
```
```
!github-translations  |  Sends a link to the Github Translations page of I.Rule:
!github-translations
```

### 6. Sending links to wiki pages:
```
!wiki  |  Sends the link to the I.Rule wiki
```
```
!wiki-baby  |  Sends the link to the I.Rule Babies wiki subpage
```
```
!wiki-item  |  Sends the link to the I.Rule Items wiki subpage
```
```
!wiki-enemies  |  Sends the link to the I.Rule Enemies wiki subpage
```
```
!wiki-bosses  |  Sends the link to the I.Rule Bosses wiki subpage
```
```
!wiki-modes  |  Sends the link to the I.Rule Game Modes wiki subpage
```
```
!wiki-floors  |  Sends the link to the I.Rule Floors wiki subpage
```
```
!wiki-obstacles  |  Sends the link to the I.Rule Obstacles wiki subpage
```
```
!wiki-achievements  |  Sends the link to the I.Rule Achievements wiki subpage
```
```
!wiki-pills  |  Sends the link to the I.Rule Pills wiki subpage
```
```
!wiki-updates  |  Sends the link to the I.Rule Update History wiki subpage
```
```
!wiki-effects  |  Sends the link to the I.Rule Effects wiki subpage
```

### 7. Admin only commands:
#### Delete commands:
```
!delete-baby  |  Deletes the baby by entering the babies id or name:
!delete-baby id/name  ->  where id is the discord message id
```
```
!delete-item  |  Deletes the item by entering the items id or name:
!delete-item id/name  ->  where id is the discord message id
```
```
!delete-item-active  |  Deletes the active item by entering the items id or name:
!delete-item-active id/name  ->  where id is the discord message id
```
```
!delete-enemy  |  Deletes the enemy by entering the enemies id or name:
!delete-enemy id/name  ->  where id is the discord message id
```
```
!delete-baby-all  |  Deletes all babies with rating lower than the entered rateNo:
!delete-baby-all ratingNo 
```
```
!delete-item-all  |  Deletes all items with rating lower than the entered rateNo:
!delete-item-all ratingNo
```
```
!delete-item-active-all  |  Deletes all active items with rating lower than the entered rateNo:
!delete-item-active-all ratingNo
```
```
!delete-enemy-all  |  Deletes all enemies with rating lower than the entered rateNo:
!delete-enemy-all ratingNo
```
```
!delete-all  |  Deletes all ideas with rating lower than the entered rateNo:
!delete-all ratingNo
```
#### Translations commands:
```
!translation-sync  |  Syncronizes the bots translation files with the translations on the I.Rule translations Github page:
!translation-sync  ->  Can only sync translations which were already added to the bot by the translations add command
```
```
!translation-add  |  Adds a translation to the bot which then can be synced with Github using the translations sync command:
!translation-add en  ->  Adding only works if the added translation already exists on the I.Rule translations Github page
```
#### Emergency commands:
```
!killbot  |  Instantly kills the bot
```
