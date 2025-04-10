function initializeDatagrid(datagridId) {
    const datagrid = document.querySelector(`[data-grid-id="${datagridId}"]`);

    if (!datagrid) {
        return;
    }
    
    const cols = datagrid.querySelectorAll('th');

    [].forEach.call(cols, function (col) {
        if (col.classList.contains('resizing-ignored')) {
            return;
        }

        if (col.getElementsByClassName('tridenton-ui-datagrid-column-resizer').length !== 0) {
            return;
        }

        // Add a resizer element to the column
        const resizer = document.createElement('div');
        resizer.classList.add('tridenton-ui-datagrid-column-resizer');

        // Set the height
        resizer.style.height = datagrid.offsetHeight + 'px';

        col.appendChild(resizer);

        createResizableColumn(col, resizer);
    });
}

function createResizableColumn(col, resizer) {
    let x = 0;
    let w = 0;

    const mouseDownHandler = function (e) {
        x = e.clientX;

        const styles = window.getComputedStyle(col);
        w = parseInt(styles.width, 10);

        document.addEventListener('mousemove', mouseMoveHandler);
        document.addEventListener('mouseup', mouseUpHandler);

        resizer.classList.add('tridenton-ui-datagrid-column-resizer-active');
    };

    const mouseMoveHandler = function (e) {
        const dx = e.clientX - x;
        col.style.width = (w + dx) + 'px';
    };

    const mouseUpHandler = function () {
        resizer.classList.remove('tridenton-ui-datagrid-column-resizer-active');
        document.removeEventListener('mousemove', mouseMoveHandler);
        document.removeEventListener('mouseup', mouseUpHandler);
    };

    resizer.addEventListener('mousedown', mouseDownHandler);
}