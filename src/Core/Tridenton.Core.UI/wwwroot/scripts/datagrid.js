function getDatagridColumnsCount(datagridId){
    const datagrid = document.getElementById(datagridId);
    
    if (!datagrid) {
        return 0;
    }

    let columns = datagrid.getElementsByTagName('th');
    
    return columns.length;
}

function initializeDatagrid(datagridId) {
    const datagrid = document.getElementById(datagridId);

    if (!datagrid) {
        return {
            Columns: 0,
        };
    }

    let columns = datagrid.getElementsByTagName('th');

    return {
        Columns: columns.length,
    };
}