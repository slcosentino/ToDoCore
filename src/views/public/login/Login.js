import React from 'react'

import {
  CButton,
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CRow,
  CAlert,
  CSpinner
} from '@coreui/react'
import { useForm } from 'react-hook-form';
import { useSelector } from 'react-redux';
import { useAccount } from '../../../hooks/useAccount';
import { FormAlerts } from '../../../components/CoreUi/FormAlerts';

const Login = () => {
  const { register, handleSubmit, formState: { errors } } = useForm();
  const {loading} = useSelector(state => state.ui) 
  const { loginApp } = useAccount();

  const onSubmit = (credentials) => {   
    loginApp(credentials); 
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <div className="c-app c-default-layout flex-row align-items-center">
        <CContainer>
          <CRow className="justify-content-center">

            <CCard className="p-4">
              <CCardBody>
                <h1>Login</h1>
                <p className="text-muted">Sign In to your account</p>
                <div >
                  <input
                    type="text"
                    className="form-control col-md-12"
                    placeholder="Username"
                    {...register("userName", { required: { value: true, message: "The field username is required." } })}
                  />
                  <div className="mt-1" style={{ color: "red" }} role="alert">
                    {errors?.userName?.message}
                  </div>
                </div>
                <div>

                  <input
                    type="password"
                    className="form-control col-md-12"
                    placeholder="Password"
                    {...register("password", { required: { value: true, message: "The field password is required." } })}
                  />
                  <div className="mt-1" style={{ color: "red" }} role="alert">
                    {errors?.password?.message}
                  </div>
                </div>

                <CRow>
                  
                  <CCol xs="6">
                    <button type="submit" className="mt-2 btn btn-primary" disabled={loading}>
                      <CSpinner component="span" size="sm" aria-hidden="true" style={{display: loading ? "": "none"}}/>
                      <span style={{display: !loading ? "": "none"}}>Login</span>
                      <span style={{display: loading ? "": "none"}}>&nbsp;Loading...</span>
                    </button>
                  </CCol>

                  <CCol xs="6" className="text-right">
                    <CButton color="link" className="px-0" disabled={loading}>Forgot password?</CButton>
                  </CCol>
                </CRow>

                <FormAlerts />

              </CCardBody>
            </CCard>
          </CRow>
        </CContainer>
      </div>

    </form>
  )
}

export default Login
