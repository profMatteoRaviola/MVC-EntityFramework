$(document).ready(function () {
    alert("Ciao");
    $("#jsGrid").jsGrid({
        
        width: "100%", //la griglia occupa tutto lo spazio del div

        //la griglia mi permette di fare CRUD, ordinare, paginare con 10pagine per volta e filtrare le tuple
        inserting: true,
        editign: true,
        sorting: true,
        paging: true,
        filtering: true,

        autoload: true,
        pageLoading: true,
        pageSize: 10,

        controller: { //vado ad agganciare la chimata ajax, con la corrispoettiva API rest, al bottone della griglia
            loadData: function (filter) {
                /*
                 *Quando si carica la pagina, vengono mostrati tutti i clienti.
                 *Infatti utilizza l'API col verbo GET senza uno specifico id del cliente                 
                 */
                return $.ajax({
                    type: "get",
                    url: "/api/clienti",
                    data: filter,
                    contentType: "application/json"
                });
            },
            insertItem: function (item) {
                /*
                 * Richiama l'API sol verbo POST, quindi inserisce un cliente creato a partire dai dati
                 * che la griglia mi fa inserire
                 */
                return $.ajax({
                    type: "post",
                    url: "/api/clienti",
                    data: JSON.stringify(item),
                    contentType: "application/json",
                    dataType: "json"
                });
            },
            updateItem: function (item) {
                /*
                 * Richiama l'API sul verbo PUT, quindi modifica un cliente a partire dai dati che la griglia mi fa inserire
                 */
                return $.ajax({
                    type: "put",
                    url: "/api/clienti" + item.id,
                    data: JSON.stringify(item),
                    contentType: "application/json",
                    dataType: "json"
                });
            },
            deleteItem: function (item) {
                /*
                 * Richiama l'API sul verbo DELETE, quindi cancella un cliente a partire dall' id che ho fornito
                 */
                return $.ajax({
                    type: "delete",
                    url: "/api/clienti" + item.id,
                    contentType: "application/json",
                    dataType: "json"
                });
            }
        },

        //property del model cliente che si vuole facciano parte della griglia
        fields: [
            { name: "id", type: "number", width: 50 /*larghezza della colonna*/, title: "Id" },
            { name: "nome", type: "text", width: 150, title: "Nome", validate: "required" },
            { name: "cognome", type: "text", width: 150, title: "Cognome", validate: "required" },
            { type: "control" } /*colonna con le icone per le operazioni CRUD*/
        ]
    });
    alert("finito jsGrid");
});