function onLoad()
{
    GetFurnishedList();
}

const furnishedDiv = document.getElementById("getFurnished");

function GetFurnishedList() {
    fetch('https://localhost:44302/api/Residential/GetFurnished')
        .then(response => response.json())
        .then(data => {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                furnishedDiv.innerHTML +=
                    `
               <div class="b-card">
                        <img class="b-img" src="${data[i].Pictures}">
                        <p class="b-head" > ${ data[i].Title}</p><br>
                        <p class="b-text">${data[i].Price}</p>
               </div>
                     `
            }

        })
}