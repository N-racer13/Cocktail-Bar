Cocktail Bar
Contains Unity scripts for hand rehabilitation game called 'Cocktail Bar'. Game interfaces with rehabilitation device for haptic feedback. All scripts written in C#.
General idea of the game is 4 liquid dispensers appear. User has to squeeze the dispenser to fill the glasses underneath. of the 4 different liquids, every dispenser has a different resistance.
- Game Manager: spawns new glasses to be filled
- Drop Destruction: liquid is simulated as spheres to save computation power, this script destroys spheres out of sight
- Glass Fill: adjust glass shader to make them appear as being filled with liquid
- Player Movement: moves player and also adjust player riging model (forward kinematics)
- Score: keeps track of glasses filled
- Valve System: changes dimensions of valves to make them appear as being squeezed
