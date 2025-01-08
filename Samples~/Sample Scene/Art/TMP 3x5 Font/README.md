## 3x5 Pixel Perfect Font

# CHANGING THE ASSET:
- Edit the png file in a program like Aseprite
- Use this website: https://yal.cc/r/20/pixelfont/ and import the new file
    - Tile width: 3
    - Tile height: 5
- Click Save TTF, rename the file, and move it into to unity
- Inside Unity, change the the font import settings to
    - Font Size: 16
    - Rendering Mode: Hinted Raster
    - Character: Unicode
- Then go to Window -> TextMeshPro -> Font Asset Creator, and use these settings:
    - Source Font File: your source font file
    - Sampling Point Size: Auto Sizing
    - Padding: 5
    - Character Set: Unicode Range (Hex)
    - Render Mode: RASTER_HINTED
- Click Generate Font Atlas
- Then Click Save As... and you should get a functioning Pixel-Perfect TextMeshPro font asset