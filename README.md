# sCIRCLE

### sCIRCLE (single-Cell Interactive Real-time Computer-visualization for Low-dimensional Exploration) 
is a standalone Windows application for visual data mining and exploration of scRNA-Seq data sets and functional annotation data. 

## 💻 Prerequisites
You need a Windows partition to run the software. Additionally a 3-button mouse and a keyboard are recommended. 

## ➡️ Getting Started
For a quick start, load the latest release on the right, unpack it and start the .exe file included in the download. 
To get a first look at the software load the three example files from the example folder in this repository and press "Start Visualization"

## 📺 Tutorial


## 📂 Import File Formatting
If you want to import your own data set, you have to follow certain conventions in order to make sCIRCLE read your data set correctly.


### Count Matrix
| Locus Tag  | CellID1 | CellID2 |....
| ---------- | ------------- | ------------|-----------|
| GeneID1        | Expression Count  | Expression Count  |...
| GeneID2        | Expression Count  | Expression Count  | ...
| ...        | ...               | ...               |...

### Phenotypic Data
| Categories  | CellID1 | CellID2 |....
| ---------- | ------------- | ------------|-----------|
| e.g. timepoints| OD.1  | OD.2  |...
| e.g. batches    | 1  | 2  | ...
| ...        | ...               | ...               |...

### Gene Annotation File
The gene annotation file can be loaded in common GTF format or a an excel or csv table. If importing a table use the following format:
| Locus Tags | Start | End | Common Gene Name | eg. KEGG Pathways |eg. GO-terms |G_UserDefinedGeneGroup|....
| ---------- | ------------- | ------------|-----------|-----------|-----------|-----------|-----------|
| Gene ID1 | eg. 0  | eg. 250 | gene name 1 | PathwayID1,PathwayID2 |GO1,GO2,GO3...|0|...
| Gene ID2 | eg. 251 | eg. 500  |gene name 2  | PathwayID3,PathwayID4 |GO5,GO6,GO7...|1|...
| ...        | ...               | ...               |...|...|...|...|...

Multiple entries in one cell have to be separated with a comma. Gene groups can be imported with the annotation file to be used as a filter inside the software. To define a user group the column name has to start with "G_". All genes that shall be part of the group have to be marked with a "1" all other genes with a "0". The user can add as many gene groups as needed.

# 🔧Tools & Functions:

## Import & Export

## Dimensionality Reduction 
The tool features three different means of dimensionality reduction UMAP, PCA and self-imported dimensionality reduced matrices.
The counts can be log transformed before feeding them into any of the dimensionality reduction algorithms.

### PCA
The principal component analysis is computed using the [Accord Statistics .net nuget](http://accord-framework.net).
3-dimensional or 2-dimensional PCA can be visualized. The components can be swapped freely between the three axis and if more than three components are computed, it is possible to switch back and forth between different combinations of components. 

### UMAP
A 3d UMAP is computed using the [C# UMAP nuget](https://github.com/curiosity-ai/umap-sharp).

### Exporting a dimensionality reduced matrix
Via the export tab it is possible to export the currently displayed dimensionality matrix as a CSV-file.

### Importing a dimensionality reduced matrix
Custom matrices can be imported instead of using the on-board dimensionality reduction algorithms. This can either be previously saved CSV-files from inside the tool itself or dimensionality reduction matrices that were calculated with different tools. The only prerequisite is that the number of cells corresponds with the number of cells in the currently loaded data set.

## Nested Interactive Plots


## Gene Filtering
There is a filter menu for filtering and sorting for genes of interest in the upper right corner. 
In total it contains to autonomous filter banks. One for filtering genes per single cell and one for filtering genes for log2FoldChanges.
Both contain six different filters in total:


### Highest Expression / Most Up- & Downregulated Genes
Filters for a specified number of genes with the highest expression in the currently selected cell. When filtering log2FoldChanges this option filters for a number of most up- and downregulated genes.

### Area on the Genome
Filters for genes that are inside a specific are on the genome specified in kilobytes. 

### Conditional Filter
Mathematical operators (<, > & =) can be used to filter for genes with certain gene expression values.

### Descending Order
Ranks the genes according to the highest expression or most up and downregulated and reorders them accordingly.

### Filter for Metadata
Any metadata category can be used to filter for other genes that have the same specified entry in one of the metadata categories. 
Examples for the application of that filter would be filtering for a certain Cog Category, KEGG Pathway or GO-term.

### Filter for Gene Group
This filter can be used to filter for pre-imported gene groups, which the user has defined in the gene annotation file. This could be used to filter for marker genes, pathogenicity islands or genes of interest in the specific experiment. This filter can also be used to load a gene set that has been saved inside the application..

### Saving a Gene Group
You can save any set of genes, with all filters, which are currently applied, and with a custom name. From the moment the gene set is saved it is available within the "Filter for Gene Group" option.

### Modular Filter System
All Filters are chainable in any combination and the order of the filters is swappable with the arrow keys next to the respective filter. The same filter can also be chained multiple time, to filter for different metadata categories for example or to filter for a range of gene expression values by applying two conditional filters.

![scRNA-Seq_UI05 Kopie](https://github.com/BarquistLab/sCIRCLE/assets/46606031/9c07da5c-6031-4924-8e95-1ade60e71721)


## Metadata Inspector
On the upper left corner the metadata inspector can be opened. It queries all metadata annotations from the currently selected gene in real-time.
If too many metadata categories are annotated for a gene the window becomes scrollable. If too many entries are annotated for a category the rows become scrollable using Ctrl+Mouse wheel.
When clicking on a metadata category in the left column the category is automatically selected to display metadata connections for this category in the visualization option.
Clicking one of the categories also reveals the colour-coded legend for the metadata connections.
When an entry of a certain metadata category is clicked it is automatically assigned as a filter term, when using a metadata filter with that category later on. Clicking another entry in the same category overwrites the filter term.
![scRNA-Seq_UI01 Kopie](https://github.com/BarquistLab/sCIRCLE/assets/46606031/b85132f4-54a6-454b-9f2b-749174e12ede)

## Visualization options
In the visualization tab it is possible to scale the cells or the whole plot, to display imported cell IDs or to hide and unhide the coordinate system.
Additionally, the cell-colouring can be changed based on one of the imported categories like time points, different batches, pre-computed clusters or custom selected clusters inside the software using the "Painting Clusters" tool. Furthermore, it is possible to colour the cells based on the expression values of the currently selected gene across all cells. 


## Painting Clusters
With this option it is possible to assign cell groups and conditions manually. This can be used to mark certain clusters of genes by hand or select outliers to export them for further analysis. When activating this feature Alt+Mouse wheel can be used to dial in the brush size. Clicking one of the added colours and then clicking on cells with the brush assigns them to a colour. The different painted or hand-selected clusters can be named and then saved for later use or to export them.

## Fold Changes
If fold changes are activated, logFoldChanges between two cells or two metacells can be calculated and displayed. For filtering the displayed genes in the logFoldChange graph a dedicated filter bank can be used, which is explained above. To select two cells for comparison they have to be selected by clicking them with the middle mouse button while hovering. The logFoldChange will always be calculated using the second last clicked cell and comparing it to the last clicked cell. When pressing "L" it is possible to display only the logFoldChange chart centered on the screen.

## Metacells
When the metacell feature is activated one metacell is computed per condition of the selected category. For example if a pre-defined category with different time points is selected, one metacell will be computed per time point. The user can select if per gene the mean or median is calculated across all cells inside the specific condition.
Inside the software metacells can be used like normal single-cells. This means the gene expression of metacells can be filtered and logFoldChanges between different metacells can be calculated. This makes it possible to compare the data set on a condition level.


## VR
A simple VR viewer mode is activatable if VR glasses are connected using OpenVR. The VR viewer makes it possible to look at the 3d scatter plot scale and rotate it, select certain cells and genes and look at logFoldChanges. The user interface is currently only useable in the desktop version..

# 🔑 Navigation and Shortcuts

## Mouse Navigation
* Hold LMB and drag for rotating the camera around a target
* Hold MMB and drag for dragging the camera
* Hold RMB and drag for moving the camera forward and backward
* Hold LMB while pressing CTRL to zoom in and out changing the focal length of the camera
 
* Press LMB to select cells, genes etc. 
* Press MMB while hovering a cell to compare it to another cell, when log2FoldChange is active. The last two cells, which have been clicked with MMB are compared.

## Keyboard Shortcuts
* R: Reset Camera View 
* F: Focus currently hovered cell
* L: Toggles Log2FoldChange view on and off
* D: Toggle visibility of all cells on and off
* Tab: Delete cache of current radial diagram
* Alt+Mouse Wheel: Change brush size, when in Paint Clusters mode
* Ctrl+Mouse Wheel: Side-scroll long rows in metadata inspector window
  


