   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   I EColorPickerLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ColorPickerLib;assembly=ColorPickerLibColorPickerLib 5   	   Y=clr-namespace:ColorPickerLib.Controls;assembly=ColorPickerLibColorPickerLib.Controls 5   	   L HUnity3.Eyedropper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:Unity3.Eyedropper;assembly=Unity3.EyedropperUnity3.Eyedropper 5   	    EasyDraw   EasyDraw.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   Exctk4clr-namespace:ColorPickerLib;assembly=ColorPickerLib     5   	   Rcontrols=clr-namespace:ColorPickerLib.Controls;assembly=ColorPickerLib     5   	   Q
eyedropper:clr-namespace:Unity3.Eyedropper;assembly=Unity3.Eyedropper     5   	     � Title$  EasyDraw��5   	   $	��626��6   $	��979��6'   ���	#FFACACAC � Icon$ drawicon.ico��6J    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$	 New��5
      6'     5   
   -   5        $
 Open��5      6(     5   
   -   5        $
 Save��5      6(   .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��_File��6   .@�5      m� -   5         m�Name$ 	NewButton��5       m� Command$	 New��6.   m� 5      -   5        $ 
LoadButton��5      $
 Open��6/   m� 5      -   5        $ 
SaveButton��5      $
 Save��6/   m� 5      -   5        $ SaveAsButton��5      $��Save As��6J   m� 5      -   5        $ ExportButton��5      $��_Export��6L   �� 5      m� 5      -	   5        $ 
ExitButton��5      $��E_xit��6H   5      m� 5      $	��Pen��6   .@�5      m� -
   5        $ ColorButton��5      $��Color��6J   m� 5      $
��Mode��6   .@�5      m� -   5        $ PenMode��5      $	��Pen��60    m� IsCheckable . 6P   m� 5      -   5        $ 
EraserMode��5      $��Eraser��63    . 6V   m� 5      -   5        $ 
SelectMode��5      $��Select��63    . 6V   m� 5      -   5        $ SEraserMode��5      $��Stroke Eraser��64    . 6^   5       �� 5!      m� 5"      $��Pen Size��6   {�.  6-   �� 5#      -   5         ��Name$ 	brushSize��5#       � Minimum$ 1Q�6,   	 � Maximum$	 10Q�6^   $	��200��6k   
 � Value$
 1Q�6w   5$      m� 5%      $��Canvas��6   .@�5&      m� -   5        $ ClearButton��5&      $��Clear��6J   m� 5'      -   5        $ RecognizeButton��5'      $��
_Recognize��6R   m� 5(      -   5        $ AllowTouchMenuItem��5(      $��Allow touches��67    .  m� 	IsChecked . 6�   5)      5*      � 5+   
   ���	#FFF0F0F0 � ColumnDefinitions 5,      �� 5-      $	��200 �6#   61   �� 5.      $��* �6#   6/   .+�50      )  #ColorPickerLib.Controls.ColorCanvas  -   5          Name$ _colorCanvas��50   #   $	��Top=�69   $	��200��6Q   �� 51      -   5         ��Name$ AllowTouchCheckbox��51      $��Allow touches��63   $
��Left=�6K   $��
10,204,0,0q�6f   $	��Top=�6z    d� 	IsChecked$
 TrueY�6�   7  1System.Windows.Forms.Integration.WindowsFormsHost  52      $	��Top=�6    $��Right=�68   $��
0,200,10,0q�6T      Child. 53      "  Unity3.Eyedropper.EyeDropper  -   5        
eyeDropper��53   <   54      �� 55      -   5        $ 
mainSlider��55      $
��Left=�6)   $��	7,230,7,0q�6D   $	��Top=�6W   $ 1Q�6o   $	 10Q�6�   $	��186��6�   $
 1Q�6�   �� 56      -   5         ��Name$ 	inkCanvas��56      $��1��6+   57      5@   
   5A      