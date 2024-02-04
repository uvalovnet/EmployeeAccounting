async function openModalWindowTimesheet(but) {
    const td = but.parentNode;
    const tr = td.parentNode;
    const id = tr.getElementsByClassName("rowIdTimesheet")[0].innerHTML;
    const orderId = id;
    const response = await fetch(`Timesheet/${id}`, {
        method: 'GET',
    });

    let timesheet = await response.json();
    fillModalWindowTimesheet(timesheet);
}
async function openModalWindowEmployee(but) {
    const td = but.parentNode;
    const tr = td.parentNode;
    const id = tr.getElementsByClassName("rowIdEmployee")[0].innerHTML;
    const response = await fetch(`Employees/${id}`, {
        method: 'GET',
    });

    let employee = await response.json();
    fillModalWindowEmployee(employee);
}

async function buttonCreateTimesheetRow() {
    const employeeId = document.getElementById("employeeCreate").value;
    const reasonId = document.getElementById("reasonCreate").value;
    const startdate = document.getElementById("startDateCreate").value;
    const duration = document.getElementById("durationCreate").value;
    const description = document.getElementById("descriptionCreate").value;
    const discounted = document.getElementById("discauntedCreate");
    document.getElementById("employeeCreate").value = "";
    document.getElementById("reasonCreate").value = "";
    document.getElementById("startDateCreate").value = "";
    document.getElementById("durationCreate").value = "";
    document.getElementById("descriptionCreate").value = "";
    document.getElementById("discauntedCreate").value = "";
    await createTimesheetRow(employeeId, reasonId, startdate, duration, description, discounted.checked);
}
async function buttonCreateEmployee() {
    const last_name = document.getElementById("employeeLastNameCreate").value;
    const first_name = document.getElementById("employeeFirstNameCreate").value;
    document.getElementById("employeeLastNameCreate").value = "";
    document.getElementById("employeeFirstNameCreate").value = "";

    await createEmployee(last_name, first_name);
}

async function buttonUpdateTimesheetRow() {
    const id = document.getElementById('timesheetIdUpdate').innerText;
    const employee = document.getElementById("employeeUpdate").value;
    const reason = document.getElementById("reasonUpdate").value;
    const startDate = document.getElementById("startDateUpdate").value;
    const duration = document.getElementById("durationUpdate").value;
    const description = document.getElementById("descriptionUpdate").value;
    const check = document.getElementById("discountedUpdate");
    const discounted = check.checked;

    await updateTimesheetRow(id, employee, reason, startDate, duration,  description, discounted);
}
async function buttonUpdateEmployee() {
    const id = document.getElementById('employeeIdUpdate').innerText;
    const last_name = document.getElementById("employeeLastNameUpdate").value;
    const first_name = document.getElementById("employeeFirstNameUpdate").value;

    await updateEmployee(id, last_name, first_name);
}

async function createTimesheetRow(employeeId, reasonId, startdate, duration, description, discounted) {
    const response = await fetch("Timesheet/Create", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            EmployeeId: employeeId,
            ReasonId: reasonId,
            StartDate: startdate,
            Duration: duration,
            Discounted: discounted,
            Desc: description,
        })
    });
}
async function createEmployee(last_name, first_name) {

    const response = await fetch("Employees/Create", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Last_name: last_name,
            First_name: first_name,
        })
    });
}

async function buttonDeleteTimesheetRow(but) {
    const td = but.parentNode;
    const tr = td.parentNode;
    const id = tr.getElementsByClassName("rowIdTimesheet")[0].innerHTML;
    const response = await fetch(`Timesheet/Delete/${id}`, {
        method: 'DELETE',
    });
    await getTimesheetData();
}
async function buttonDeleteEmployee(but) {
    const td = but.parentNode;
    const tr = td.parentNode;
    const id = tr.getElementsByClassName("rowIdEmployee")[0].innerHTML;
    const rowsQty = tr.getElementsByClassName("qtyRows")[0].innerHTML;
    const response = await fetch(`Employees/Delete/${id};${rowsQty}`, {
        method: 'DELETE',
    });
    await getEmployeestData();
}

async function updateTimesheetRow(id, employee, reason, startDate, duration, description, discounted) {
    const response = await fetch("Timesheet/Update", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Id: id,
            EmployeeId: employee,
            ReasonId: reason,
            StartDate: startDate,
            Duration: duration,
            Discounted: discounted,
            Desc: description,
        })
    });
    await getTimesheetData();
}
async function updateEmployee(id, last_name, first_name) {
    const response = await fetch("Employees/Update", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Id: id,
            Last_name: last_name,
            First_name: first_name,
        })
    });
    await getEmployeestData();
}


async function getTimesheetData() {
    const response = await fetch("/Timesheet", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const items = await response.json();
        fillTableTimesheet(items);
    }
}
async function getEmployeestData() {
    const response = await fetch("/Employees", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const items = await response.json();
        fillTableEmployees(items);
    }
}
async function getDataForSelectors() {
    const response1 = await fetch("/Employees", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    const response2 = await fetch("/Reasons", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response1.ok === true & response2.ok === true) {
        const employees = await response1.json();
        const reasons = await response2.json();
        fillSelectors(employees, reasons);
    }
}



async function fillTableTimesheet(items) {
    let parentElement = document.getElementById('viewTable');

    while (parentElement.firstChild) {
        parentElement.removeChild(parentElement.firstChild);
    }


    if (items.length > 0) {

        const thead = document.createElement("thead");
        const tr1 = document.createElement("tr");
        const thId = document.createElement("th");
        const thEmployee = document.createElement("th");
        const thReason = document.createElement("th");
        const thStartDate = document.createElement("th");
        const thDuration = document.createElement("th");
        const thDiscounted = document.createElement("th");
        const thActions = document.createElement("th");

        thId.innerHTML = "Id";
        thEmployee.innerHTML = "Сотрудник";
        thReason.innerHTML = "Причина";
        thStartDate.innerHTML = "Дата начала";
        thDuration.innerHTML = "Продолжительность";
        thDiscounted.innerHTML = "Учтено при оплате";
        thActions.innerHTML = "Действия";

        tr1.append(thId);
        tr1.append(thEmployee);
        tr1.append(thReason);
        tr1.append(thStartDate);
        tr1.append(thDuration);
        tr1.append(thDiscounted);
        tr1.append(thActions);
        thead.append(tr1);

        const tbody = document.createElement("tbody");
        for (let i = 0; i < items.length; i++) {
            tr2 = document.createElement("tr");
            tdId = document.createElement("td");
            tdId.setAttribute("class", "rowIdTimesheet");
            tdEmployee = document.createElement("td");
            tdReason = document.createElement("td");
            tdStartDate = document.createElement("td");
            tdDuration = document.createElement("td");
            tdDiscounted = document.createElement("td");
            tdActions = document.createElement("td");

            let but1 = document.createElement("button");
            but1.setAttribute("class", "btn btn-warning");
            but1.setAttribute("data-bs-toggle", "modal");
            but1.setAttribute("data-bs-target", "#EditorModal");
            but1.innerHTML = "Редактировать";

            but1.addEventListener('click', function (e) {
                openModalWindowTimesheet(e.target);
            })

            let but2 = document.createElement("button");
            but2.setAttribute("class", "btn btn-danger");
            but2.innerHTML = "Удалить";

            but2.addEventListener('click', function (e) {

                buttonDeleteTimesheetRow(e.target);
            })

            tdId.innerHTML = items[i]["id"];
            tdEmployee.innerHTML = items[i]["employee"];
            tdReason.innerHTML = items[i]["reason"];
            tdStartDate.innerHTML = items[i]["startDate"].split("T")[0];
            tdDuration.innerHTML = items[i]["duration"];
            tdDiscounted.innerHTML = items[i]["discounted"];

            tdActions.append(but1);
            tdActions.append(but2);
            tr2.append(tdId);
            tr2.append(tdEmployee);
            tr2.append(tdReason);
            tr2.append(tdStartDate);
            tr2.append(tdDuration);
            tr2.append(tdDiscounted);
            tr2.append(tdActions);
            tbody.append(tr2);
        }
        parentElement.append(thead);
        parentElement.append(tbody);

    }

    return parentElement;
}
async function fillTableEmployees(items) {
    let parentElement = document.getElementById('viewTable');

    while (parentElement.firstChild) {
        parentElement.removeChild(parentElement.firstChild);
    }


    if (items.length > 0) {

        const thead = document.createElement("thead");
        const tr1 = document.createElement("tr");
        const thId = document.createElement("th");
        const thLastName = document.createElement("th");
        const thFirstName = document.createElement("th");
        const thQtyRows = document.createElement("th");
        const thActions = document.createElement("th");

        thId.innerHTML = "Id";
        thLastName.innerHTML = "Фамилия";
        thFirstName.innerHTML = "Имя";
        thQtyRows.innerHTML = "Количество пропусков";
        thActions.innerHTML = "Действия";

        tr1.append(thId);
        tr1.append(thLastName);
        tr1.append(thFirstName);
        tr1.append(thQtyRows);
        tr1.append(thActions);
        thead.append(tr1);

        const tbody = document.createElement("tbody");
        for (let i = 0; i < items.length; i++) {
            tr2 = document.createElement("tr");
            tdId = document.createElement("td");
            tdId.setAttribute("class", "rowIdEmployee");
            tdLastName = document.createElement("td");
            tdFirstName = document.createElement("td");
            tdQtyRows = document.createElement("td");
            tdQtyRows.setAttribute("class", "qtyRows");
            tdActions = document.createElement("td");

            let but1 = document.createElement("button");
            but1.setAttribute("class", "btn btn-warning");
            but1.setAttribute("data-bs-toggle", "modal");
            but1.setAttribute("data-bs-target", "#EditorModal2");
            but1.innerHTML = "Редактировать";

            but1.addEventListener('click', function (e) {
                openModalWindowEmployee(e.target);
            })

            let but2 = document.createElement("button");
            but2.setAttribute("class", "btn btn-danger");
            but2.innerHTML = "Удалить";

            but2.addEventListener('click', function (e) {

                buttonDeleteEmployee(e.target);
            })

            tdId.innerHTML = items[i]["id"];
            tdLastName.innerHTML = items[i]["last_name"];
            tdFirstName.innerHTML = items[i]["first_name"];
            tdQtyRows.innerHTML = items[i]["rowsQty"];


            tdActions.append(but1);
            tdActions.append(but2);
            tr2.append(tdId);
            tr2.append(tdLastName);
            tr2.append(tdFirstName);
            tr2.append(tdQtyRows);
            tr2.append(tdActions);
            tbody.append(tr2);
        }
        parentElement.append(thead);
        parentElement.append(tbody);

    }

    return parentElement;
}
async function fillSelectors(employees, reasons) {
    let parentElement = document.getElementById('employeeCreate');
    let parentElement2 = document.getElementById('reasonCreate');
    let parentElement3 = document.getElementById('employeeUpdate');
    let parentElement4 = document.getElementById('reasonUpdate');

    if (employees.length > 0) {

        for (let i = 0; i < employees.length; i++) {
            const option = document.createElement("option");
            option.setAttribute("value", employees[i]["id"]);
            option.innerHTML = employees[i]["last_name"] + " "+ employees[i]["first_name"];

            parentElement.append(option);
        }

        for (let i = 0; i < employees.length; i++) {
            option = document.createElement("option");
            option.setAttribute("value", employees[i]["id"]);
            option.innerHTML = employees[i]["last_name"] + " " + employees[i]["first_name"];

            parentElement3.append(option);
        }
    }

    if (reasons.length > 0) {

        for (let i = 0; i < reasons.length; i++) {
            const option = document.createElement("option");
            option.setAttribute("value", reasons[i]["id"]);
            option.innerHTML = reasons[i]["name"];

            parentElement2.append(option);
        }

        for (let i = 0; i < reasons.length; i++) {
            option = document.createElement("option");
            option.setAttribute("value", reasons[i]["id"]);
            option.innerHTML = reasons[i]["name"];

            parentElement4.append(option);
        }
    }
}

async function fillModalWindowTimesheet(timesheet_row) {
    document.getElementById('timesheetIdUpdate').innerText = timesheet_row["id"];
    document.getElementById('employeeUpdate').value = timesheet_row["employeeId"];
    document.getElementById("reasonUpdate").value = timesheet_row["reasonId"];
    document.getElementById('startDateUpdate').value = timesheet_row["startDate"].split("T")[0];
    document.getElementById('durationUpdate').value = timesheet_row["duration"];
    document.getElementById("descriptionUpdate").value = timesheet_row["desc"];
    let checkBox = document.getElementById('discauntedUpdate');
    checkBox = timesheet_row["discounted"];
}
async function fillModalWindowEmployee(employee) {
    document.getElementById('employeeIdUpdate').innerText = employee["id"];
    document.getElementById('employeeLastNameUpdate').value = employee["last_name"];
    document.getElementById("employeeFirstNameUpdate").value = employee["first_name"];
}




document.getElementById("createTimesheetRow").addEventListener("click", async () => {
    await buttonCreateTimesheetRow();
});
document.getElementById("createEmployee").addEventListener("click", async () => {
    await buttonCreateEmployee();
});

document.getElementById("saveUpdateChangesTimesheet").addEventListener("click", async () => {
    await buttonUpdateTimesheetRow();
});
document.getElementById("saveUpdateChangesEmployee").addEventListener("click", async () => {
    await buttonUpdateEmployee();
});

document.getElementById("timesheetOpenTable").addEventListener("click", async () => {
    await getTimesheetData();
});
document.getElementById("employeeOpenTable").addEventListener("click", async () => {
    await getEmployeestData();
});


window.addEventListener('load', () => {
    getDataForSelectors();
});