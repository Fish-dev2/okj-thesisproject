   M S B A M L   `   `   `  ????C  ?Recaller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:NagyProjektNagyProjekt  5   	       NagyProjekt.JatekAblak   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:NagyProjekt     5   	     ? Title$  
JatekAblak??5   	   $	??450??6   $	??800??6)    ? 
ResizeMode$ NoResize=?65    ? WindowStyle$
 None=?6K   
???   ? ? 	Resources 5
      !  NagyProjekt.LoszerConverter   loszerconverter  NagyProjekt.HelyConverter  helyconverter#  NagyProjekt.EletSzinConverter  eletszinconverter??%W   &	        &	      &	 &       5   
   6:     5   
   66     5   
   6>   5      .??5      ?? -   5         ??Name$ 
fővászon??5      .+?5   
   ?? -   5         ??Name$ fegyvertext??5      $??	ExtraBold"?6)   ???	#4D000000
??? d ?$??15(?6n   |?.   K? ZIndex$ 5??6  $??Verdana)?6  
 ?? Top 6?   ?? *Jatekos.Hely.Y+ ?? 	Converter# [ 	 ?? Left	 6?   ?? *Jatekos.Hely.X+??5      `? 5      
 ?? StringFormat$
 	{0} - {1}??6   .1?5      ??  ?? Path$% Jatekos.JelenFegyver.FegyverNev?6   ?? 5      $! Jatekos.JelenFegyver.Loszer?6   # [  6B   5      5      ? 5   
   -   5         ?Name$ eletbar??5      
?????? $??LeftToRight=?6?   $??10??6   ? Minimum$ 0Q?6  $ 5??6s  ??6@   ?? *EletPont.JelenErtek+# [  6?   ?? *Hely.Y+	 6?   ?? *Hely.X+??6?   ?? *	Meret.X+ ? Maximum 6&  ?? *EletPont.MaxErtek+ ? Value 6L  ?? *EletPont.JelenErtek+6s  ?? 5   
   -   5         ??Name$ 	mainstack??5   .    ?? Orientation$ 
Horizontal=?6   .+?5      ?? -   5        $ balPanel??5       J? Opacity$ 1Q?6+   .+?5      ?? 
???????
?????? $??Weapons??6D   $??Verdana)?6V   $??35(?6k   $??Center=?6z   ?? 5      
 ? Tag$ 	clickable??6   ???	#ADFFFFFF
?????? $??Shotgun??6X   $??Verdana)?6j   $??25(?6   $??Center=?6?   ?? 5      $ 	clickable??6   ???	#ADFFFFFF
?????? $??Sniper??6X   $??Verdana)?6i   $??25(?6~   $??Center=?6?   ?? 5      $ 	clickable??6   ???	#ADFFFFFF
?????? $	??LMG??6X   $??Verdana)?6f   $??25(?6{   $??Center=?6?   ?? 5      $ 	clickable??6   ???	#ADFFFFFF
?????? $??Cannon??6X   $??Verdana)?6i   $??25(?6~   $??Center=?6?   5       ?? 5!      -   5        $ 
kozepPanel??5!      $ 1Q?6-   .+?5"      ?? 
???????
?????? $??Skills??6D   $??Verdana)?6U   $??35(?6j   $??Center=?6y   ?? 5#      ???	#ADFFFFFF
?????? $??Choose One:??6H   $??Verdana)?6^   $??25(?6s   $??Center=?6?   ?? 5$      $ 	clickable??6   ???	#ADFFFFFF
?????? $??	Fire Rate??6X   $??Verdana)?6l   $??25(?6?   $??Center=?6?   ?? 5%      $ 	clickable??6   ???	#ADFFFFFF
?????? $??Movement Speed??6X   $??Verdana)?6q   $??25(?6?   $??Center=?6?   ?? 5&      $ 	clickable??6   ???	#ADFFFFFF
?????? $??Health Points??6X   $??Verdana)?6p   $??25(?6?   $??Center=?6?   5'      ?? 5(      -   5        $ 	jobbPanel??5(      $ 1Q?6,   .+?5)      ?? 
???????
?????? $??Defenses??6D   $??Verdana)?6W   $??35(?6l   $??Center=?6z   6?   ?? 5*      ???	#ADFFFFFF
?????? $??Crate??6H   $??Verdana)?6X   $??25(?6m   $??Center=?6{   6?   ?? 5+      ???	#ADFFFFFF
?????? $??	Cost: 200??6H   $??Verdana)?6\   $??25(?6q   $??Center=?6   6?   5-      ?? 5.      -   5        $ 	pénztext??5.   $   $ 1Q?6   $??Center=?66   $??	ExtraBold"?6Q   ???	#4D000000
???????$??15(?6?   |?.  $ 5??6?   $??Verdana)?6?   ??5/      `? 50      $
 	Cash: {0}??6#   .1?51      ?? $
 Penz?6"   52      54      ?? 55      -	   5         ??Name$ nextwavebutt??55   (   $??	Next Wave??6   $??	ExtraBold"?6>   ???	#4D000000
???????$??15(?6   |?.  $ 5??6?   $??Verdana)?6?   $??Center=?6?   6?   56      58      59      