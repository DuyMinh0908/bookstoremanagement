   M S B A M L   `   `   `  ????O  KbookStoreManagetment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:bookStoreManagetmentbookStoreManagetment  5   	   )    #bookStoreManagetment.BarcodeScanner   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:bookStoreManagetment     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6N     ? Title$  Quét Barcode??5	   	   $	??400??6   $	??600??6+    ? WindowStartupLocation$ CenterOwner=?5
   	   $??Medium"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	   ???	#FF495B67???#F2F7FA   MaterialDesignFont#???   5   	     
DSHoaDonVM ? DataContext# [ 5   	   .??5      ?  ? RowDefinitions 5   
   ?? 5      $??9* ?6   6*   ?? 5      $??2* ?6   6*   .+?5   
   ? $??0??6    5      ?? 5      $??7* ?6    6.   ?? 5      $??* ?6    6-   .+?5      #  MaterialDesignThemes.Wpf.Card  $??
10 10 10 0q?6"   $??0??66   .??5      ?? -   5         ??Name$ videoPlayer??5      ??6-   ?? *QRCode+5      ?? 5        ?? Orientation$ 
Horizontal=?6   $??1??62   $
??Left=?5!      .+?5"      ?? $??30??6   $??30??5#      $??0q?5$      
?????? 
?????? ???#1e98d7??5(      ?? *StartBarcodeScannerCommand+.??5)      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Play=?6.   $??30??5*   2   $??30??5+   2   5,      ?? 5-      $??30??6   $??30??5.      $??0q?5/      
?????? 
?????? 
???  ??$??5 0 5 0q?54      ??53      ?? *StopBarcodeScannerCommand+.??55        $ Pause=?6.   $??30??56   2   $??30??57   2   58      59      5:      ? 5;   
   $??1??6   $??10 0 0 0q?6   .+?5<      ?? $ 
Horizontal=?6   .+?5=      ?? .+?5>      ?? $??Chọn camera:??6    $??16(?5?       $??SemiBold"?5@       $??0 0 0 5q?5A       63     5B      $	??200??6*   $??25??66   .??5C      ?? -   5        	 ??Name$	 comboBox??5C   #   $	??200??5D      $??25??5E      $
??Left=?5F      $??Center=?5G      
 ?? DisplayMemberPath$

 Name??5H      ??5I      ?? *VideoDevices+ ?? SelectedItem 5J      ?? *CurrentDevice+5K      5L      ?? 5N      $??10 0 0 0q?6   .+?5O      ?? $??Kết quả:??6    $??16(?5P       $??SemiBold"?5Q       $??0 0 0 5q?5R       63     5S      $	??200??6*   $??25??66   .??5T      ?? $??Center=?5V      $	??200??5W      $??25??6!   ??5U      ?? *	Barcode+ ?? Mode$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=?5Y      5Z      5[      5\      5]      5_      5`      