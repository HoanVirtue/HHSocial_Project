const rememberMeNonchecked = document.getElementById("remember-me-nonchecked")
const rememberMeChecked = document.getElementById("remember-me-checked")

rememberMeNonchecked.addEventListener("click", (e) => {
    rememberMeNonchecked.classList.toggle("hidden")
    rememberMeChecked.classList.toggle("hidden")
})

rememberMeChecked.addEventListener("click", (e) => {
    rememberMeNonchecked.classList.toggle("hidden")
    rememberMeChecked.classList.toggle("hidden")

    rememberMeNonchecked.checked = false
})
