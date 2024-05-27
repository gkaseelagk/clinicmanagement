using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicmanagement.Models;
using clinicmanagement.ViewModel;

namespace clinicmanagement.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ClinicDbContext _context;

        public PatientsController(ClinicDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var clinicViewModel = new ClinicViewModel
            {
                Patients = await _context.Patients.ToListAsync(),
                Doctors = await _context.Doctors.ToListAsync(),
                Appointments = await _context.Appointments.ToListAsync()
            };
            return View(clinicViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit()
        {
            // Perform any necessary operations with the form data

            // Redirect to the ThankYou view
            return View("Submit");
        }

        // GET: Patient/Create
        public IActionResult Create()
        {
            var clinicViewModel = new ClinicViewModel
            {
                Patients = _context.Patients.ToList(),
                Doctors = _context.Doctors.ToList(),
                Appointments = _context.Appointments.ToList(),

            };
            ViewBag.PatientId = new SelectList(clinicViewModel.Patients, "Id", "Name");
            ViewBag.DoctorId = new SelectList(clinicViewModel.Doctors, "Id", "Name");
            return View(clinicViewModel);
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Patient,Doctor,Appointment")] ClinicViewModel clinicViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(clinicViewModel.Patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            clinicViewModel.Doctors = await _context.Doctors.ToListAsync();
            clinicViewModel.Appointments = await _context.Appointments.ToListAsync();
            return View(clinicViewModel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            var clinicViewModel = new ClinicViewModel
            {
                Patient = patient,
                Doctors = await _context.Doctors.ToListAsync(),
                Appointments = await _context.Appointments.ToListAsync()
            };
            return View(clinicViewModel);
        }

        // Other CRUD actions for Patient like Edit, Details, Delete can be implemented similarly...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Patient")] ClinicViewModel clinicViewModel)
        {
            if (id != clinicViewModel.Patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinicViewModel.Patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(clinicViewModel.Patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            clinicViewModel.Doctors = await _context.Doctors.ToListAsync();
            clinicViewModel.Appointments = await _context.Appointments.ToListAsync();
            return View(clinicViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
        // GET: Patients

    }
}
    

