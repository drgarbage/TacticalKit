# TacticalKit

[About]
TacticalKit is an app tool for Airsoft player to sync intel on battlefield.
This app share intel by display location & direction of each player at same side.
This app won't provide enemy intel to another team. When more than 1 team 
use this app on same field, their intel should be secure.

I hope players can trust this service, as long as they didn't expose their team
code to enemy, the server must not design any way to show intel to opposite team.


[Defines]
Battle := A root of game states for a team.
The Leader of team should create a Battle, for his member to join. Game states 
in different Battle won't be able to share. If two team play in same location,
they should create two Battle to seperate the intel of each team.

BattleToken := An authentication token for api call.

Group := A define of sub team in a Battle.
A Battle can contain unlimited group, each group will define its own name.
Group name can be define as a path, such as following examples:

    US.AF.101SQUAD.ALPHA
    US.NAVY.SEAL.BRAVO
    US.NAVY

Each gammer has his own group name, they can see all other gammer's location on
map with 50% opacity. And only those gammer with exact same group name will be
shown as 100% opacity.

Gammer := A member participate in this battle.
Gammer could be a human player, drone or even simulated objects which interacts 
with the tactical map. Every gammer has its own location, direction and active 
status. Gammers won't be seen before they been apply to a group.

GammerState := { token, location, direction, status }

Intel := Non-interactable elements on map.
Intel includes graphical contents, texts, drawings and markers on the map. Which
will be synced to server and deliver to all related client in same battle.

Overlay := Static graphical content on map.
Including offline maps and grids.

StatePackage := { GammerState[] gammerstates, Intel[] intels, Overlay[] overlays }

[Server API Spec]
/battle := battle( string name ) : BattleToken
/regist := regist( Gammer gammer ) : StatePackage
/sync := sync ( StatePackage state ) : StatePackage
/update := update ( Intel[] intels ) : StatePackage
/reset := reset ( ) : StatePackage

@issue: This spec didn't consider a player with more than two device. If a player
        login more than 1 device, the map will show info for each device.
