   M S B A M L   `   `   `  ����I  Ecalculator-app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:calculator_appcalculator_app  5   	       calculator_app.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:calculator_app     5   	     � Title$  String Evaluator��5   	    � DataContext 5	      (  "calculator_app.CalculatorViewModel  5
   
   .��5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�6*   .+�5   
   �� -   5         ��Name$ input��5      $��5q�6!    �� TextWrapping$
 Wrap=�6{   $	��245��6�   $��57��6�   ��6,   �� *TextBoxText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��6�    J� InputBindings 5      �� 5      
 �� Key$ Return��6   	 �� Command	 5      �� *abc+.��5   %   5      �� 5   
   $��	Calculate��6   $��5q�6%   $��50��61   ��6=   �� *abc+�� 5   
   -   5        
 ��Name$
 output��5      $��5q�6!   $��35��6G   ��6,   �� *Result+6G   5      5      